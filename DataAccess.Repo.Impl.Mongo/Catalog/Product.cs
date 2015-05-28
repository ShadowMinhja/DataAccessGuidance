//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo.Catalog
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Product
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public string Color { get; set; }

        public decimal ListPrice { get; set; }

        public SizeUnitOfMeasure Size { get; set; }

        public WeightUnitOfMeasure Weight { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public ProductCategory Category { get; set; }
    }
}
