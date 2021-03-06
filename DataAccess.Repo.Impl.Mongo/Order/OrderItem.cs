﻿//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo.Order
{
    public class OrderItem
    {
        public short Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
