param(
    [Parameter(Mandatory=$true)][string] $sqlConnectionString,
    [Parameter(Mandatory=$true)][string] $neo4jConnectionString
)

$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition
. $scriptPath\ValidationFunctions.ps1

Write-Host "Extracting product recommendations from SQL Server to Neo4j..." -ForegroundColor Yellow

#$valid = IsSupportedVersionOfPowerShell
#if (!$valid)
#{
    #Write-Host "Script requires PowerShell 3.0" -ForegroundColor Red
    #exit
#}

$neo4jBatchUrl = "$neo4jConnectionString/db/data/batch"

$productCommandText = "SELECT p.ProductID, p.Name
                         FROM eCommerce.Production.Product p
                        WHERE p.ProductSubcategoryID IS NOT NULL
                        ORDER BY p.ProductID"

$recommendationSqlCommandText = "SELECT r.PurchasedProductID, r.PurchasedProductName, r.RecommendedProductID, r.RecommendedProductName,
       r.TotalRecommendedSales, p.TotalPurchasedSales,
       ROUND(CAST(r.TotalRecommendedSales AS float) / CAST(p.TotalPurchasedSales AS float), 4) as Percentage,
       r.Ranking
  FROM (
         SELECT sod.ProductID PurchasedProductID, p.Name PurchasedProductName,
                     sod2.ProductID RecommendedProductID, p2.Name RecommendedProductName,
                COUNT(sod2.ProductID) TotalRecommendedSales,
                ROW_NUMBER() OVER(PARTITION BY sod.ProductID ORDER BY count(sod2.ProductID) desc) as Ranking
           FROM Sales.SalesOrderHeader soh INNER JOIN Sales.SalesOrderDetail sod
                  ON soh.SalesOrderID = sod.SalesOrderID INNER JOIN Sales.SalesOrderDetail sod2
                    ON soh.SalesOrderID = sod2.SalesOrderID
                    AND sod.ProductID != sod2.ProductID INNER JOIN Production.Product p
                    ON sod.ProductID = p.ProductID INNER JOIN Production.Product p2
                    ON sod2.ProductID = p2.ProductID
             GROUP BY sod.ProductID, p.Name, sod2.ProductID, p2.Name
          ) r INNER JOIN (
                           SELECT sod.ProductID, COUNT(sod.ProductID) TotalPurchasedSales
                                          FROM Sales.SalesOrderDetail sod
                                        GROUP BY sod.ProductID
                                   ) p
    ON r.PurchasedProductID = p.ProductID
WHERE r.Ranking <= 5
ORDER BY r.PurchasedProductID, r.Ranking"

$cypherDeleteQueryJson = "{ ""query"" : ""START n = node(*) MATCH n-[r?]-() WHERE ID(n) <> 0 DELETE n, r"" }"

$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = $sqlConnectionString
$selectCmd = $sqlConnection.CreateCommand()
$selectCmd.CommandType = [System.Data.CommandType]::Text
$selectCmd.CommandText = $productCommandText

$dataAdapter = New-Object System.Data.SqlClient.SqlDataAdapter
$dataAdapter.SelectCommand = $selectCmd
$ds = New-Object System.Data.DataSet

try
{
    $rowsAffected = $dataAdapter.Fill($ds, "Products")
    $dataAdapter.SelectCommand.CommandText = $recommendationSqlCommandText
    $rowsAffected = $dataAdapter.Fill($ds, "Recommendations")

    #Delete all nodes and relationships
    #try
    #{
    #    Write-Host "Deleting all nodes and relationships..." -ForegroundColor Yellow
    #    Invoke-RestMethod -Uri $neo4jConnectionString/db/data/cypher -Method POST -ContentType application/json -Body $cypherDeleteQueryJson | Out-Null
    #    Write-Host "Deletion successful." -ForegroundColor Green
    #}
    #catch [System.Net.WebException]
    #{
    #    Write-Error ([System.String]::Format("Error deleting product nodes and relationships: {0}", $_.Exception.Message))
    #    exit
    #}

    try
    {
        Write-Host "Deleting product_id_index..." -ForegroundColor Yellow
        #Delete node index, if it exists
        Invoke-RestMethod -Uri $neo4jConnectionString/db/data/index/node/product_id_index -Method Delete | Out-Null
        Write-Host "Index deleted." -ForegroundColor Green
    }
    catch [System.Net.WebException]
    {
        #Just eat the 404.  There has GOT to be a better way to check than this.
        if (([System.Net.HttpWebResponse]([System.Net.WebException]([System.Management.Automation.ErrorRecord]$_).Exception).Response).StatusCode -ne [System.Net.HttpStatusCode]::NotFound)
        {
            Write-Error ([System.String]::Format("Error creating product_id_index: {0}", $_.Exception.Message))
            exit
        }
    }

    #Clear out the Neo4j database
    Write-Host "Creating product_id_index..." -ForegroundColor Yellow
    #Create node index
    Invoke-RestMethod -Uri $neo4jConnectionString/db/data/index/node -Method POST -ContentType application/json -Body "{""name"":""product_id_index"",""config"":{""type"":""exact"",""provider"":""lucene""}}" | Out-Null
    Write-Host "Index created successfully." -ForegroundColor Green

    $batchCommands = New-Object System.Collections.Generic.List``1[System.String]

    $totalProductNodes = 0;
    $totalRecommendationEdges = 0;

    $recordCount = 0;
    $batchSize = 100;
    for ($i = 0; $i -lt $ds.Tables["Products"].Rows.Count; $i++)
    {
        $recordCount++
        $row = $ds.Tables["Products"].Rows[$i]

        $batchCommands.Add("{""method"":""POST"",""to"":""/node"",""body"":{""name"":""" +
        $row["Name"] + """,""productId"":" + $row["ProductID"] + "},""id"":" + $i + "}")

        #Add node to index
        $batchCommands.Add("{""method"":""POST"",""to"":""index/node/product_id_index"",""body"":{""value"":""" +
        $row["ProductID"] + """,""uri"":""{" + $i + "}"",""key"":""productId""}}")

        if ($recordCount -eq $batchSize)
        {
            $totalProductNodes += $recordCount
            $batchBody = "[" + [System.String]::Join(",", $batchCommands) + "]"
    
            Write-Host ([System.String]::Format("Creating {0} product nodes...", $recordCount)) -ForegroundColor Yellow
            Invoke-RestMethod -Uri $neo4jBatchUrl -Method POST -ContentType application/json -Body $batchBody | Out-Null

            $recordCount = 0
            $batchCommands.Clear()
        }
    }

    #Get the leftovers
    if ($recordCount -gt 0)
    {
        $totalProductNodes += $recordCount
        $batchBody = "[" + [System.String]::Join(",", $batchCommands) + "]"
    
        Write-Host ([System.String]::Format("Creating {0} product nodes...", $recordCount)) -ForegroundColor Yellow
        Invoke-RestMethod -Uri $neo4jBatchUrl -Method POST -ContentType application/json -Body $batchBody | Out-Null
    }

    $recordCount = 0
    $batchCommands.Clear()
    $cypherRelationshipQuery = "{{""method"":""POST"",""to"":""cypher"",""body"":{{ ""query"" : ""START n1=node:product_id_index(productId={{fromProductId}}), n2=node:product_id_index(productId={{toProductId}}) CREATE n1-[:PRODUCT_RECOMMENDATION{{props}}]->n2"", ""params"" : {{""fromProductId"":{0}, ""toProductId"":{1}, ""props"":{{""percentage"":{2}}}}} }}}}"
    foreach ($row in $ds.Tables["Recommendations"].Rows)
    {
        $recordCount++
        $batchCommands.Add([System.String]::Format($cypherRelationshipQuery, $row["PurchasedProductID"], $row["RecommendedProductID"], $row["Percentage"]))
        if ($recordCount -eq $batchSize)
        {
            $totalRecommendationEdges += $recordCount
            $batchBody = "[" + [System.String]::Join(",", $batchCommands) + "]"
    
            Write-Host ([System.String]::Format("Creating {0} recommendation relationships...", $recordCount)) -ForegroundColor Yellow
            Invoke-RestMethod -Uri $neo4jBatchUrl -Method POST -ContentType application/json -Body $batchBody | Out-Null

            $recordCount = 0
            $batchCommands.Clear()
        }
    }

    if ($recordCount -gt 0)
    {
        $totalRecommendationEdges += $recordCount
        $batchBody = "[" + [System.String]::Join(",", $batchCommands) + "]"

        Write-Host ([System.String]::Format("Creating {0} recommendation relationships...", $recordCount)) -ForegroundColor Yellow
        Invoke-RestMethod -Uri $neo4jBatchUrl -Method POST -ContentType application/json -Body $batchBody | Out-Null
    }

    Write-Host ([System.String]::Format("{0} product nodes created successfully.", $totalProductNodes)) -ForegroundColor Green
    Write-Host ([System.String]::Format("{0} recommendation edges created successfully.", $totalRecommendationEdges)) -ForegroundColor Green
}
catch [System.Data.SqlClient.SqlException]
{
    Write-Error ([System.String]::Format("Error filling DataSet: {0}", $_.Exception.Message))
}

Write-Host "Recommendation extract complete." -ForegroundColor Green
# SIG # Begin signature block
# MIIasQYJKoZIhvcNAQcCoIIaojCCGp4CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUaU5jqlDGYVmdOrHQQoWi7cIg
# KRGgghWCMIIEwzCCA6ugAwIBAgITMwAAADQkMUDJoMF5jQAAAAAANDANBgkqhkiG
# 9w0BAQUFADB3MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSEw
# HwYDVQQDExhNaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EwHhcNMTMwMzI3MjAwODI1
# WhcNMTQwNjI3MjAwODI1WjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNO
# OkI4RUMtMzBBNC03MTQ0MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5RoHrQqWLNS2
# NGTLNCDyvARYgou1CdxS1HCf4lws5/VqpPW2LrGBhlkB7ElsKQQe9TiLVxj1wDIN
# 7TSQ7MZF5buKCiWq76F7h9jxcGdKzWrc5q8FkT3tBXDrQc+rsSVmu6uitxj5eBN4
# dc2LM1x97WfE7QP9KKxYYMF7vYCNM5NhYgixj1ESZY9BfsTVJektZkHTQzT6l4H4
# /Ieh7TlSH/jpPv9egMkGNgfb27lqxzfPhrUaS0rUJfLHyI2vYWeK2lMv80wegyxj
# yqAQUhG6gVhzQoTjNLLu6pO+TILQfZYLT38vzxBdGkVmqwLxXyQARsHBVdKDckIi
# hjqkvpNQAQIDAQABo4IBCTCCAQUwHQYDVR0OBBYEFF9LQt4MuTig1GY2jVb7dFlJ
# ZoErMB8GA1UdIwQYMBaAFCM0+NlSRnAK7UD7dvuzK7DDNbMPMFQGA1UdHwRNMEsw
# SaBHoEWGQ2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3Rz
# L01pY3Jvc29mdFRpbWVTdGFtcFBDQS5jcmwwWAYIKwYBBQUHAQEETDBKMEgGCCsG
# AQUFBzAChjxodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY3Jv
# c29mdFRpbWVTdGFtcFBDQS5jcnQwEwYDVR0lBAwwCgYIKwYBBQUHAwgwDQYJKoZI
# hvcNAQEFBQADggEBAA9CUKDVHq0XPx8Kpis3imdYLbEwTzvvwldp7GXTTMVQcvJz
# JfbkhALFdRxxWEOr8cmqjt/Kb1g8iecvzXo17GbX1V66jp9XhpQQoOtRN61X9id7
# I08Z2OBtdgQlMGESraWOoya2SOVT8kVOxbiJJxCdqePPI+l5bK6TaDoa8xPEFLZ6
# Op5B2plWntDT4BaWkHJMrwH3JAb7GSuYslXMep/okjprMXuA8w6eV4u35gW2OSWa
# l4IpNos4rq6LGqzu5+wuv0supQc1gfMTIOq0SpOev5yDVn+tFS9cKXELlGc4/DC/
# Zef1Od7qIu2HjKuyO7UBwq3g/I4lFQwivp8M7R0wggTsMIID1KADAgECAhMzAAAA
# sBGvCovQO5/dAAEAAACwMA0GCSqGSIb3DQEBBQUAMHkxCzAJBgNVBAYTAlVTMRMw
# EQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVN
# aWNyb3NvZnQgQ29ycG9yYXRpb24xIzAhBgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNp
# Z25pbmcgUENBMB4XDTEzMDEyNDIyMzMzOVoXDTE0MDQyNDIyMzMzOVowgYMxCzAJ
# BgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25k
# MR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xDTALBgNVBAsTBE1PUFIx
# HjAcBgNVBAMTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAOivXKIgDfgofLwFe3+t7ut2rChTPzrbQH2zjjPmVz+l
# URU0VKXPtIupP6g34S1Q7TUWTu9NetsTdoiwLPBZXKnr4dcpdeQbhSeb8/gtnkE2
# KwtA+747urlcdZMWUkvKM8U3sPPrfqj1QRVcCGUdITfwLLoiCxCxEJ13IoWEfE+5
# G5Cw9aP+i/QMmk6g9ckKIeKq4wE2R/0vgmqBA/WpNdyUV537S9QOgts4jxL+49Z6
# dIhk4WLEJS4qrp0YHw4etsKvJLQOULzeHJNcSaZ5tbbbzvlweygBhLgqKc+/qQUF
# 4eAPcU39rVwjgynrx8VKyOgnhNN+xkMLlQAFsU9lccUCAwEAAaOCAWAwggFcMBMG
# A1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBRZcaZaM03amAeA/4Qevof5cjJB
# 8jBRBgNVHREESjBIpEYwRDENMAsGA1UECxMETU9QUjEzMDEGA1UEBRMqMzE1OTUr
# NGZhZjBiNzEtYWQzNy00YWEzLWE2NzEtNzZiYzA1MjM0NGFkMB8GA1UdIwQYMBaA
# FMsR6MrStBZYAck3LjMWFrlMmgofMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6Ly9j
# cmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY0NvZFNpZ1BDQV8w
# OC0zMS0yMDEwLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKGPmh0dHA6
# Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljQ29kU2lnUENBXzA4LTMx
# LTIwMTAuY3J0MA0GCSqGSIb3DQEBBQUAA4IBAQAx124qElczgdWdxuv5OtRETQie
# 7l7falu3ec8CnLx2aJ6QoZwLw3+ijPFNupU5+w3g4Zv0XSQPG42IFTp8263Os8ls
# ujksRX0kEVQmMA0N/0fqAwfl5GZdLHudHakQ+hywdPJPaWueqSSE2u2WoN9zpO9q
# GqxLYp7xfMAUf0jNTbJE+fA8k21C2Oh85hegm2hoCSj5ApfvEQO6Z1Ktwemzc6bS
# Y81K4j7k8079/6HguwITO10g3lU/o66QQDE4dSheBKlGbeb1enlAvR/N6EXVruJd
# PvV1x+ZmY2DM1ZqEh40kMPfvNNBjHbFCZ0oOS786Du+2lTqnOOQlkgimiGaCMIIF
# vDCCA6SgAwIBAgIKYTMmGgAAAAAAMTANBgkqhkiG9w0BAQUFADBfMRMwEQYKCZIm
# iZPyLGQBGRYDY29tMRkwFwYKCZImiZPyLGQBGRYJbWljcm9zb2Z0MS0wKwYDVQQD
# EyRNaWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkwHhcNMTAwODMx
# MjIxOTMyWhcNMjAwODMxMjIyOTMyWjB5MQswCQYDVQQGEwJVUzETMBEGA1UECBMK
# V2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
# IENvcnBvcmF0aW9uMSMwIQYDVQQDExpNaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBD
# QTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALJyWVwZMGS/HZpgICBC
# mXZTbD4b1m/My/Hqa/6XFhDg3zp0gxq3L6Ay7P/ewkJOI9VyANs1VwqJyq4gSfTw
# aKxNS42lvXlLcZtHB9r9Jd+ddYjPqnNEf9eB2/O98jakyVxF3K+tPeAoaJcap6Vy
# c1bxF5Tk/TWUcqDWdl8ed0WDhTgW0HNbBbpnUo2lsmkv2hkL/pJ0KeJ2L1TdFDBZ
# +NKNYv3LyV9GMVC5JxPkQDDPcikQKCLHN049oDI9kM2hOAaFXE5WgigqBTK3S9dP
# Y+fSLWLxRT3nrAgA9kahntFbjCZT6HqqSvJGzzc8OJ60d1ylF56NyxGPVjzBrAlf
# A9MCAwEAAaOCAV4wggFaMA8GA1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFMsR6MrS
# tBZYAck3LjMWFrlMmgofMAsGA1UdDwQEAwIBhjASBgkrBgEEAYI3FQEEBQIDAQAB
# MCMGCSsGAQQBgjcVAgQWBBT90TFO0yaKleGYYDuoMW+mPLzYLTAZBgkrBgEEAYI3
# FAIEDB4KAFMAdQBiAEMAQTAfBgNVHSMEGDAWgBQOrIJgQFYnl+UlE/wq4QpTlVnk
# pDBQBgNVHR8ESTBHMEWgQ6BBhj9odHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtp
# L2NybC9wcm9kdWN0cy9taWNyb3NvZnRyb290Y2VydC5jcmwwVAYIKwYBBQUHAQEE
# SDBGMEQGCCsGAQUFBzAChjhodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2Nl
# cnRzL01pY3Jvc29mdFJvb3RDZXJ0LmNydDANBgkqhkiG9w0BAQUFAAOCAgEAWTk+
# fyZGr+tvQLEytWrrDi9uqEn361917Uw7LddDrQv+y+ktMaMjzHxQmIAhXaw9L0y6
# oqhWnONwu7i0+Hm1SXL3PupBf8rhDBdpy6WcIC36C1DEVs0t40rSvHDnqA2iA6VW
# 4LiKS1fylUKc8fPv7uOGHzQ8uFaa8FMjhSqkghyT4pQHHfLiTviMocroE6WRTsgb
# 0o9ylSpxbZsa+BzwU9ZnzCL/XB3Nooy9J7J5Y1ZEolHN+emjWFbdmwJFRC9f9Nqu
# 1IIybvyklRPk62nnqaIsvsgrEA5ljpnb9aL6EiYJZTiU8XofSrvR4Vbo0HiWGFzJ
# NRZf3ZMdSY4tvq00RBzuEBUaAF3dNVshzpjHCe6FDoxPbQ4TTj18KUicctHzbMrB
# 7HCjV5JXfZSNoBtIA1r3z6NnCnSlNu0tLxfI5nI3EvRvsTxngvlSso0zFmUeDord
# EN5k9G/ORtTTF+l5xAS00/ss3x+KnqwK+xMnQK3k+eGpf0a7B2BHZWBATrBC7E7t
# s3Z52Ao0CW0cgDEf4g5U3eWh++VHEK1kmP9QFi58vwUheuKVQSdpw5OPlcmN2Jsh
# rg1cnPCiroZogwxqLbt2awAdlq3yFnv2FoMkuYjPaqhHMS+a3ONxPdcAfmJH0c6I
# ybgY+g5yjcGjPa8CQGr/aZuW4hCoELQ3UAjWwz0wggYHMIID76ADAgECAgphFmg0
# AAAAAAAcMA0GCSqGSIb3DQEBBQUAMF8xEzARBgoJkiaJk/IsZAEZFgNjb20xGTAX
# BgoJkiaJk/IsZAEZFgltaWNyb3NvZnQxLTArBgNVBAMTJE1pY3Jvc29mdCBSb290
# IENlcnRpZmljYXRlIEF1dGhvcml0eTAeFw0wNzA0MDMxMjUzMDlaFw0yMTA0MDMx
# MzAzMDlaMHcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xITAf
# BgNVBAMTGE1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQTCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAJ+hbLHf20iSKnxrLhnhveLjxZlRI1Ctzt0YTiQP7tGn
# 0UytdDAgEesH1VSVFUmUG0KSrphcMCbaAGvoe73siQcP9w4EmPCJzB/LMySHnfL0
# Zxws/HvniB3q506jocEjU8qN+kXPCdBer9CwQgSi+aZsk2fXKNxGU7CG0OUoRi4n
# rIZPVVIM5AMs+2qQkDBuh/NZMJ36ftaXs+ghl3740hPzCLdTbVK0RZCfSABKR2YR
# JylmqJfk0waBSqL5hKcRRxQJgp+E7VV4/gGaHVAIhQAQMEbtt94jRrvELVSfrx54
# QTF3zJvfO4OToWECtR0Nsfz3m7IBziJLVP/5BcPCIAsCAwEAAaOCAaswggGnMA8G
# A1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFCM0+NlSRnAK7UD7dvuzK7DDNbMPMAsG
# A1UdDwQEAwIBhjAQBgkrBgEEAYI3FQEEAwIBADCBmAYDVR0jBIGQMIGNgBQOrIJg
# QFYnl+UlE/wq4QpTlVnkpKFjpGEwXzETMBEGCgmSJomT8ixkARkWA2NvbTEZMBcG
# CgmSJomT8ixkARkWCW1pY3Jvc29mdDEtMCsGA1UEAxMkTWljcm9zb2Z0IFJvb3Qg
# Q2VydGlmaWNhdGUgQXV0aG9yaXR5ghB5rRahSqClrUxzWPQHEy5lMFAGA1UdHwRJ
# MEcwRaBDoEGGP2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1
# Y3RzL21pY3Jvc29mdHJvb3RjZXJ0LmNybDBUBggrBgEFBQcBAQRIMEYwRAYIKwYB
# BQUHMAKGOGh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljcm9z
# b2Z0Um9vdENlcnQuY3J0MBMGA1UdJQQMMAoGCCsGAQUFBwMIMA0GCSqGSIb3DQEB
# BQUAA4ICAQAQl4rDXANENt3ptK132855UU0BsS50cVttDBOrzr57j7gu1BKijG1i
# uFcCy04gE1CZ3XpA4le7r1iaHOEdAYasu3jyi9DsOwHu4r6PCgXIjUji8FMV3U+r
# kuTnjWrVgMHmlPIGL4UD6ZEqJCJw+/b85HiZLg33B+JwvBhOnY5rCnKVuKE5nGct
# xVEO6mJcPxaYiyA/4gcaMvnMMUp2MT0rcgvI6nA9/4UKE9/CCmGO8Ne4F+tOi3/F
# NSteo7/rvH0LQnvUU3Ih7jDKu3hlXFsBFwoUDtLaFJj1PLlmWLMtL+f5hYbMUVbo
# nXCUbKw5TNT2eb+qGHpiKe+imyk0BncaYsk9Hm0fgvALxyy7z0Oz5fnsfbXjpKh0
# NbhOxXEjEiZ2CzxSjHFaRkMUvLOzsE1nyJ9C/4B5IYCeFTBm6EISXhrIniIh0EPp
# K+m79EjMLNTYMoBMJipIJF9a6lbvpt6Znco6b72BJ3QGEe52Ib+bgsEnVLaxaj2J
# oXZhtG6hE6a/qkfwEm/9ijJssv7fUciMI8lmvZ0dhxJkAj0tr1mPuOQh5bWwymO0
# eFQF1EEuUKyUsKV4q7OglnUa2ZKHE3UiLzKoCG6gW4wlv6DvhMoh1useT8ma7kng
# 9wFlb4kLfchpyOZu6qeXzjEp/w7FW1zYTRuh2Povnj8uVRZryROj/TGCBJkwggSV
# AgEBMIGQMHkxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xIzAh
# BgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNpZ25pbmcgUENBAhMzAAAAsBGvCovQO5/d
# AAEAAACwMAkGBSsOAwIaBQCggbIwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQw
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFHzi
# dkVSvLfWdwx85PHuRrXNz5RVMFIGCisGAQQBgjcCAQwxRDBCoBqAGABwAG4AcABk
# AGEAZwBzAGMAcgBpAHAAdKEkgCJodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcHJh
# Y3RpY2VzMA0GCSqGSIb3DQEBAQUABIIBAFlC0UXBaaPQx/Nqc69bt6hTI83W1eTi
# /vHelcF0ghRXOODfDB73HlVVemEc1wjJqnmaBNqws+z55cgWNDhnJxBYuoX7pTUB
# UZK0+RZr9hT6TKi8kWH7edSddf2ZxNMtsPQHt7y1/HpBge4RNzYlYZ2A/3EKu3Qv
# AS+enIhDJBjIsr9irIe5ihwvjlgmftNeyDF3H7XuRUF7kGB2Fzn8c1BdS+0+3J8i
# qgecUeP5OkqUSh7d9sgpmzOoO1Soqqo89oejHRYaxgu0woWXV1LKSdX81TZnCfOZ
# COcmSkeu1XqAbt0X9BwdMoF4LzZwTbdLu5OW73E9BXscFdCCVgP13SOhggIoMIIC
# JAYJKoZIhvcNAQkGMYICFTCCAhECAQEwgY4wdzELMAkGA1UEBhMCVVMxEzARBgNV
# BAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jv
# c29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWljcm9zb2Z0IFRpbWUtU3RhbXAg
# UENBAhMzAAAANCQxQMmgwXmNAAAAAAA0MAkGBSsOAwIaBQCgXTAYBgkqhkiG9w0B
# CQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0xMzA4MTYxOTA1MTlaMCMG
# CSqGSIb3DQEJBDEWBBTspTRrbksQtga9pnTK9HqXt67c5DANBgkqhkiG9w0BAQUF
# AASCAQDCZxLvVRy8kM8h6MpJeBxSsIqChYLpjkZRH03phszCiN3UBiv7FBOVJvSs
# 3tj8WXbDl+guDyWL6SM/GiP7zI1Z7kfKX2m8CIh5piqyv+H6BaAeY9K8K3V9y9uw
# CLYZYQvVDygsIQwLNdJP9GKg8BwewFTuc+5LrSz1cjulLfTFbnCS4ByGNS/fro5+
# E5syufpm206lSHybBTLCZjLXHDZ91h4ibSMA/rTCQo5S2kG97p0HmV/C/mpbeWbx
# xek6iHgSGP4MCav7ezL6SEJZH0itDLW8i0WpRjur9N3iBjFR7jhCmbKQ9MDYfIps
# +LOC4xaMSDqGCRF87uDzX4/GrIFk
# SIG # End signature block
