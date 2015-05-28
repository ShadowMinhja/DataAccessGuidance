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
    
    public class SalesOrderHeaderMap : EntityTypeConfiguration<SalesOrderHeader>
    {
        public SalesOrderHeaderMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("SalesOrderHeader", "Sales");

            // define the primary key for the table
            this.HasKey(soh => soh.SalesOrderId);

            // set the required relationship with the SalesOrderDetail child collection
            this.HasMany(soh => soh.SalesOrderDetails)
                .WithRequired(sod => sod.SalesOrderHeader)
                .HasForeignKey(sod => sod.SalesOrderId)
                .WillCascadeOnDelete(true);
        }
    }
}
