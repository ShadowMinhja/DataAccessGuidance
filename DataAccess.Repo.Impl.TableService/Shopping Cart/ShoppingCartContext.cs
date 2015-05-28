//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.TableService.ShoppingCart
{
    using System.Data.Services.Client;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
    using Microsoft.Azure;

    public sealed class ShoppingCartContext
    {
        private const string TableName = "ShoppingCart";
        private CloudTableClient tableClient;

        public ShoppingCartContext()
        {
            string connectionString;
            if (RoleEnvironment.IsAvailable)
            {
                connectionString = RoleEnvironment.GetConfigurationSettingValue("windowsAzureStorageConnectionString");
            }
            else
            {
                connectionString = CloudConfigurationManager.GetSetting("windowsAzureStorageConnectionString");
            }

            try
            {
                this.tableClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();
            }
            catch
            {
                this.tableClient = null;
            }
        }

        public ShoppingCartTableEntity Get(string rowKey)
        {
            var table = this.tableClient.GetTableReference(TableName);
            var partitionKey = ShoppingCartTableEntity.CalculatePartitionKey(rowKey);
            TableOperation retrieveOperation = TableOperation.Retrieve<ShoppingCartTableEntity>(partitionKey, rowKey);
            try
            {
                TableResult retrievedResult = table.Execute(retrieveOperation);
                return (ShoppingCartTableEntity)retrievedResult.Result;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We want to restrict the type passed to this method.")]
        public void Save(ShoppingCartTableEntity shoppingCart)
        {
            var table = this.tableClient.GetTableReference(TableName);
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(shoppingCart);
            table.Execute(insertOrReplaceOperation);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We want to restrict the type passed to this method.")]
        public void Delete(ShoppingCartTableEntity shoppingCart)
        {
            var table = this.tableClient.GetTableReference(TableName);
            TableOperation deleteOperation = TableOperation.Delete(shoppingCart);
            table.Execute(deleteOperation);
        }
    }
}
