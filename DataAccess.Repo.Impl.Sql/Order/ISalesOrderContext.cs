﻿//===============================================================================
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

    public interface ISalesOrderContext : IDisposable
    {
        IDbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        IDbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        int SaveChanges();
    }
}