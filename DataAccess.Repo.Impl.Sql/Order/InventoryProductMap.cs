//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.Order
{
    using System.Data.Entity.ModelConfiguration;
    using DataAccess.Repo.Impl.Sql;

    public class InventoryProductMap : EntityTypeConfiguration<InventoryProduct>
    {
        public InventoryProductMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("Product", "Production");

            // define the primary key for the table
            this.HasKey(p => p.ProductId);
        }
    }
}