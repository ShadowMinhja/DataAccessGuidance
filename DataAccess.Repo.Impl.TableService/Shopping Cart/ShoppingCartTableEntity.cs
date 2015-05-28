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
    using System;
    using System.Globalization;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public sealed class ShoppingCartTableEntity : TableEntity
    {
        private static int numberOfBuckets = 100;

        public ShoppingCartTableEntity(string userId)
        {
            this.RowKey = userId;
            this.PartitionKey = ShoppingCartTableEntity.CalculatePartitionKey(userId);
        }

        public ShoppingCartTableEntity()
        {
        }

        public string ShoppingCartItemsJson { get; set; }

        public Guid TrackingId { get; set; }

        public static string CalculatePartitionKey(string userId)
        {
            // We will just use the built-in hashing provided by GetHashCode() to distribute across an arbitrary number of buckets.
            // This is not really a "generic" way to generate a partition key, and should be changed to suit a particular use case and/or data.
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId cannot be null, empty, or only whitespace.");
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "ShoppingCart_{0:000}",
                (Math.Abs(userId.GetHashCode()) % ShoppingCartTableEntity.numberOfBuckets) + 1);
        }
    }
}
