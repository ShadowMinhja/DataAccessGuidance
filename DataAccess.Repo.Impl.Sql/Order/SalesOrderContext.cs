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
    using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "We need IDisposable at context's interface but base class implements it")]
    public sealed class SalesOrderContext : BaseContext<SalesOrderContext>, ISalesOrderContext
    {
        public IDbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        public IDbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new SalesOrderHeaderMap());
            modelBuilder.Configurations.Add(new SalesOrderDetailMap());
            modelBuilder.Entity<SalesOrderHeader>().Ignore(soh => soh.BillToAddress);
            modelBuilder.Entity<SalesOrderHeader>().Ignore(soh => soh.ShipToAddress);
            modelBuilder.Entity<SalesOrderHeader>().Ignore(soh => soh.CreditCard);
        }
    }
}