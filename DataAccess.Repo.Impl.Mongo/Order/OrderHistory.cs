//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo.Order
{
    using System;
    using System.Collections.Generic;
    using DataAccess.Domain.Order;
    using MongoDB.Bson.Serialization.Attributes;

    public class OrderHistory
    {
        public OrderHistory(Guid id, Guid orderCode)
        {
            this.OrderCode = orderCode;
            this.Id = id;
        }

        [BsonId]
        public Guid Id { get; set; }

        public Address BillToAddress { get; set; }

        public CreditCard CreditCard { get; set; }

        public int CustomerId { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Freight { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public Address ShippingAddress { get; set; }

        public OrderStatus Status { get; set; }
    }
}
