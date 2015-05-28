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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class SalesOrderDetailMap : EntityTypeConfiguration<SalesOrderDetail>
    {
        public SalesOrderDetailMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("SalesOrderDetail", "Sales");

            // define the primary key for the table
            this.HasKey(sod => new { sod.SalesOrderId, sod.SalesOrderDetailId });

            this.Property(sod => sod.SalesOrderId)
                .HasDatabaseGeneratedOption(null);

            this.Property(sod => sod.SalesOrderDetailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // set the required relationship for the SalesOrderHeader parent
            this.HasRequired(sod => sod.SalesOrderHeader)
                .WithMany(soh => soh.SalesOrderDetails)
                .HasForeignKey(sod => sod.SalesOrderId);
        }
    }
}