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
    public class SalesOrderDetail
    {
        public int SalesOrderId { get; set; }

        public int SalesOrderDetailId { get; set; }

        public virtual SalesOrderHeader SalesOrderHeader { get; set; }

        public short OrderQty { get; set; }

        public int ProductId { get; set; }

        public int SpecialOfferId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }
    }
}