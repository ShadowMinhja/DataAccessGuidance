<#
.SYNOPSIS
This PowerShell script sets up the various NoSQL stores for the patterns & practices Data Access Guidance reference implementation

.DESCRIPTION
This PowerShell script automates all of the scripts required to set up the Data Access Guidance reference implementation to run against the various NoSQL data stores.

.PARAMETER sqlConnectionString
Connection string to the SQL Server database that contains the product, categories, and subcategories.  This parameter is required when -createMongoProducts, -createNeo4jRecommendations, or -createSqlTrackingId is set to $true.

.PARAMETER mongoConnectionString
Connection string to the MongoDB server/cluster that will contain the products, categories, and subcategories after the import process.  This parameter is required when -createMongoProducts or -createMongoOrderHistory is set to $true.

.PARAMETER neo4jConnectionString
Connection string to the Neo4j server that will contain the product recommendations after the import process.  This parameter is required when -createNeo4jRecommendations is set to $true.

.PARAMETER windowsAzureStorageConnectionString
Connection string for the Windows Azure Storage account that should be configured for shopping cart and/or images.  This parameter is required when -createTableStorageShoppingCart or createBlobStorageImages is set to $true.  To use the Windows Azure Development Storage, set this to "Development".

.PARAMETER createMongoProducts
Set this value to $true to import the product, category, and subcategory data from SQL Server to MongoDB.

.PARAMETER createMongoOrderHistory
Set this value to $true to configure MongoDB as the data store for order history.

.PARAMETER createNeo4jRecommendations
Set this value to $true to import the product recommendations data from SQL Server to MongoDB.

.PARAMETER createTableStorageShoppingCart
Set this value to $true to configure Windows Azure Table Storage as the data store for shopping carts.

.PARAMETER createBlobStorageImages
Set this value to $true to upload the reference implementation images to Windows Azure Blob Storage.

.PARAMETER createSqlTrackingId
Set this value to $true to create the TrackingId field in the Sales.SalesOrderHeader SQL Server table.

.PARAMETER createSqlPersonGuid
Set this value to $true to create the PersonGuid field in the Person.Person SQL Server table.

#>
param(
    [Parameter(Mandatory=$false)][string]$sqlConnectionString,
    [Parameter(Mandatory=$false)][string]$mongoConnectionString,
    [Parameter(Mandatory=$false)][string]$neo4jConnectionString,
    [Parameter(Mandatory=$false)][string]$windowsAzureStorageConnectionString,
    [Parameter(Mandatory=$false)][bool] $createMongoProducts = $false,
    [Parameter(Mandatory=$false)][bool] $createMongoOrderHistory = $false,
    [Parameter(Mandatory=$false)][bool] $createNeo4jRecommendations = $false,
    [Parameter(Mandatory=$false)][bool] $createTableStorageShoppingCart = $false,
    [Parameter(Mandatory=$false)][bool] $createBlobStorageImages = $false,
    [Parameter(Mandatory=$false)][bool] $createSqlTrackingId = $false,
    [Parameter(Mandatory=$false)][bool] $createSqlPersonGuid = $false
)

#If no options are set, just exit.
if ((!$createMongoProducts) -and (!$createMongoOrderHistory) -and (!$createNeo4jRecommendations) -and (!$createTableStorageShoppingCart) -and (!$createBlobStorageImages) -and (!$createSqlTrackingId) -and (!$createSqlPersonGuid))
{
    Get-Help .\SetupDataGuidance.ps1 -Detailed
    exit
}

$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition

. $scriptPath\ValidationFunctions.ps1
. $scriptPath\NuGetFunctions.ps1

#$valid = IsSupportedVersionOfPowerShell
#if (!$valid)
#{
    #Write-Host "Script requires PowerShell 3.0" -ForegroundColor Red
    #exit
#}

if ($createMongoProducts -or $createNeo4jRecommendations -or $createSqlTrackingId -or $createSqlPersonGuid)
{
    if ([System.String]::IsNullOrWhiteSpace($sqlConnectionString))
    {
        Write-Error '-sqlConnectionString must be set if -createMongoProducts, -createNeo4jRecommendations, -createSqlTrackingId, or -createSqlPersonGuid are $true.'
        Exit
    }

    Write-Host "Validating SQL Server connection..." -ForegroundColor Yellow
    $valid = IsValidSQLServerConnectionString $sqlConnectionString
    if (!$valid)
    {
        Write-Error "Invalid SQL Server connection string."
        exit
    }
    Write-Host "Valid!" -ForegroundColor Green
    Write-Host
}

if ($createMongoProducts -or $createMongoOrderHistory)
{
    #Make sure the NuGet packages are installed.
    RestoreNuGetPackages("DataAccess.Repo.Impl.Mongo")

    if ([System.String]::IsNullOrWhiteSpace($mongoConnectionString))
    {
        Write-Error '-mongoConnectionString must be set if -createMongoProducts or -createMongoOrderHistory are set to $true.'
        Exit
    }

    Write-Host "Validating MongoDB connection..." -ForegroundColor Yellow
    $valid = IsValidMongoDBConnectionString $mongoConnectionString
    if (!$valid)
    {
        Write-Error "Unable to connect to MongoDB."
        exit
    }
    Write-Host "Valid!" -ForegroundColor Green
    Write-Host
}

if ($createNeo4jRecommendations)
{
    #Make sure the NuGet packages are installed.
    RestoreNuGetPackages("DataAccess.Repo.Impl.Neo4j")

    if ([System.String]::IsNullOrWhiteSpace($neo4jConnectionString))
    {
        Write-Error '-neo4jConnectionString must be set if -createNeo4jRecommendations is set to $true.'
        Exit
    }

    Write-Host "Validating Neo4j connection..." -ForegroundColor Yellow
    $valid = IsValidNeo4jConnectionString $neo4jConnectionString
    if (!$valid)
    {
        Write-Error "Unable to connect to Neo4j."
        exit
    }
    Write-Host "Valid!" -ForegroundColor Green
    Write-Host
}

if ($createTableStorageShoppingCart -or $createBlobStorageImages)
{
    #Make sure the NuGet packages are installed.
    RestoreNuGetPackages("DataAccess.Repo.Impl.TableService")

    if ([System.String]::IsNullOrWhiteSpace($windowsAzureStorageConnectionString))
    {
        Write-Error '-windowsAzureStorageConnectionString must be set if -createTableStorageShoppingCart or -createBlobStorageImages are set to $true.'
        Exit
    }

    $useWindowsAzureDevelopmentStorageAccount = $false
    if ($windowsAzureStorageConnectionString -ieq "Development")
    {
        $useWindowsAzureDevelopmentStorageAccount = $true
        Write-Host "Using Windows Azure Development Storage..." -ForegroundColor Green
        Write-Host
    }

    if (!$useWindowsAzureDevelopmentStorageAccount)
    {
        $valid = IsValidWindowsAzureStorageConnectionString $windowsAzureStorageConnectionString
        if (!$valid)
        {
            Write-Error "Invalid Windows Azure Storage Account connection string."
            exit
        }
    }
}

if ($createMongoProducts)
{
    & "$scriptPath\ProductsFromSqlToMongo.ps1" -sqlConnectionString $sqlConnectionString -mongoConnectionString $mongoConnectionString
    Write-Host
    & "$scriptPath\CategoriesAndSubCategoriesFromSqlToMongo.ps1" -sqlConnectionString $sqlConnectionString -mongoConnectionString $mongoConnectionString
    Write-Host
}

if ($createMongoOrderHistory)
{
    & "$scriptPath\CreateOrderHistoryMongo.ps1" -mongoConnectionString $mongoConnectionString
    Write-Host
}

if ($createNeo4jRecommendations)
{
    & "$scriptPath\ProductRecommendationsFromSqlToNeo4j.ps1" -sqlConnectionString $sqlConnectionString -neo4jConnectionString $neo4jConnectionString
    Write-Host
}

if ($createTableStorageShoppingCart)
{
    & "$scriptPath\SetupWindowsAzureTableStorage.ps1" -windowsAzureStorageConnectionString $windowsAzureStorageConnectionString -useDevelopmentStorageAccount $useWindowsAzureDevelopmentStorageAccount
    Write-Host
}

if ($createBlobStorageImages)
{
    & "$scriptPath\ProductPhotosFromDiskToBlobStorage.ps1" -windowsAzureStorageConnectionString $windowsAzureStorageConnectionString -useDevelopmentStorageAccount $useWindowsAzureDevelopmentStorageAccount
    Write-Host
}

if ($createSqlTrackingId)
{
    & "$scriptPath\AddTrackingIdToSql.ps1" -sqlConnectionString $sqlConnectionString
    Write-Host
}

if ($createSqlPersonGuid)
{
    & "$scriptPath\AddPersonGuidToSql.ps1" -sqlConnectionString $sqlConnectionString
    Write-Host
}

Write-Host "Setup complete." -ForegroundColor Green
Write-Host

# SIG # Begin signature block
# MIIayQYJKoZIhvcNAQcCoIIaujCCGrYCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUrsnP6H0JhUcKgzUPsTWgUGVX
# qSygghWCMIIEwzCCA6ugAwIBAgITMwAAADPlJ4ajDkoqgAAAAAAAMzANBgkqhkiG
# 9w0BAQUFADB3MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSEw
# HwYDVQQDExhNaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EwHhcNMTMwMzI3MjAwODIz
# WhcNMTQwNjI3MjAwODIzWjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNO
# OkY1MjgtMzc3Ny04QTc2MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyt7KGQ8fllaC
# X9hCMtQIbbadwMLtfDirWDOta4FQuIghCl2vly2QWsfDLrJM1GN0WP3fxYlU0AvM
# /ZyEEXmsoyEibTPgrt4lQEWSTg1jCCuLN91PB2rcKs8QWo9XXZ09+hdjAsZwPrsi
# 7Vux9zK65HG8ef/4y+lXP3R75vJ9fFdYL6zSDqjZiNlAHzoiQeIJJgKgzOUlzoxn
# g99G+IVNw9pmHsdzfju0dhempaCgdFWo5WAYQWI4x2VGqwQWZlbq+abLQs9dVGQv
# gfjPOAAPEGvhgy6NPkjsSVZK7Jpp9MsPEPsHNEpibAGNbscghMpc0WOZHo5d7A+l
# Fkiqa94hLwIDAQABo4IBCTCCAQUwHQYDVR0OBBYEFABYGz7txfEGk74xPTa0rAtd
# MvCBMB8GA1UdIwQYMBaAFCM0+NlSRnAK7UD7dvuzK7DDNbMPMFQGA1UdHwRNMEsw
# SaBHoEWGQ2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3Rz
# L01pY3Jvc29mdFRpbWVTdGFtcFBDQS5jcmwwWAYIKwYBBQUHAQEETDBKMEgGCCsG
# AQUFBzAChjxodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY3Jv
# c29mdFRpbWVTdGFtcFBDQS5jcnQwEwYDVR0lBAwwCgYIKwYBBQUHAwgwDQYJKoZI
# hvcNAQEFBQADggEBAAL/44wD6u9+OLm5fJ87UoOk+iM41AO4alm16uBviAP0b1Fq
# lTp1hegc3AfFTp0bqM4kRxQkTzV3sZy8J3uPXU/8BouXl/kpm/dAHVKBjnZIA37y
# mxe3rtlbIpFjOzJfNfvGkTzM7w6ZgD4GkTgTegxMvjPbv+2tQcZ8GyR8E9wK/EuK
# IAUdCYmROQdOIU7ebHxwu6vxII74mHhg3IuUz2W+lpAPoJyE7Vy1fEGgYS29Q2dl
# GiqC1KeKWfcy46PnxY2yIruSKNiwjFOPaEdHodgBsPFhFcQXoS3jOmxPb6897t4p
# sETLw5JnugDOD44R79ECgjFJlJidUUh4rR3WQLYwggTsMIID1KADAgECAhMzAAAA
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
# 9wFlb4kLfchpyOZu6qeXzjEp/w7FW1zYTRuh2Povnj8uVRZryROj/TGCBLEwggSt
# AgEBMIGQMHkxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xIzAh
# BgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNpZ25pbmcgUENBAhMzAAAAsBGvCovQO5/d
# AAEAAACwMAkGBSsOAwIaBQCggcowGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQw
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFBYF
# Q3Lq7SWkgUn7yopDuxeD5DQoMGoGCisGAQQBgjcCAQwxXDBaoDKAMABwAG4AcABk
# AGEAZwAgAHAAbwB3AGUAcgBzAGgAZQBsAGwAIABzAGMAcgBpAHAAdKEkgCJodHRw
# Oi8vd3d3Lm1pY3Jvc29mdC5jb20vcHJhY3RpY2VzMA0GCSqGSIb3DQEBAQUABIIB
# AOhlUfSH3mVfJuIGp7qtlcLsUGHesH9+233Pei7PdCbKQhOj+grxUOaDQIDmFXyz
# FeB5JfS9tDLjOfpa1YrVbpi/mLvNKNHV3vh9z0z2PFUiujUhH8n1oGut3zG1nLdO
# IqbCFKCYAjU2wynbCuXjPs1Bg88nrkt3yCsrlWPytjzJENnnfgqg1rA1Ev8Sjl6E
# e34255IcqLEOnY5PQEJlZtdLQ4iQKQnXdlF2gj8NfgPRtKTwOOAyMIaO/JcWsnu+
# 34nZA0B4aki9R4OJSWG32V7LWGXxjJQzEuFJlucMQ3If6FILtSYidoixxKCgP+D8
# /DHODIcwgVCFTPgGnUJnYcmhggIoMIICJAYJKoZIhvcNAQkGMYICFTCCAhECAQEw
# gY4wdzELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcT
# B1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UE
# AxMYTWljcm9zb2Z0IFRpbWUtU3RhbXAgUENBAhMzAAAAM+UnhqMOSiqAAAAAAAAz
# MAkGBSsOAwIaBQCgXTAYBgkqhkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3
# DQEJBTEPFw0xMzA4MTkyMDUzNDBaMCMGCSqGSIb3DQEJBDEWBBSzTIm9XF8/n6fJ
# BYsfLUT32tMfazANBgkqhkiG9w0BAQUFAASCAQDJvvtEeDDxpq0XUgIScw4eAcDk
# 9nU+8RfuCisY8UQ8lLnIRCvP1dZBrdRQJ7sPdfpHd4/8/QLXFpssxWZ2jLbJ7Xgn
# /GnSek9SpeqqePUstOUUzXN2nxtcWdETaIJI338A0DvP5KHoaY/j4g4PST9mdhj2
# IxE1bYi+8+NWTLHyqmI8L7W6u7vyXSqmuTbhaPxj4ZroFTE+q5cDHxlUowIdQDZ/
# lAzRey4w3nJk0CNUuDP8fWsZ7sKxNooLcgoIc5GsUZBNaSj54LW6C8ybsO8TB/cD
# JRoJPApefHDdkv2tgaCg7obO1dVLmSTgllFt6VVL6JgaAdtl9ltd5R99dW4n
# SIG # End signature block
