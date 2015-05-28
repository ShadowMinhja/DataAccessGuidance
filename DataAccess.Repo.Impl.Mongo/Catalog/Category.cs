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
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;

    public class Category
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}
