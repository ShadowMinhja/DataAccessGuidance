//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Neo4j.Catalog
{
    using Newtonsoft.Json;

    public class ProductGraphEntity
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }
    }
}