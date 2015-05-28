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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class SalesOrderHeader
    {
        public int SalesOrderId { get; set; }

        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public byte Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public int CustomerId { get; set; }

        public Guid TrackingId { get; set; }

        public virtual Person.PersonAddress BillToAddress { get; set; }

        public int BillToAddressId { get; set; }

        public virtual Person.PersonAddress ShipToAddress { get; set; }

        public int ShipToAddressId { get; set; }

        public int ShipMethodId { get; set; }

        public virtual Person.PersonCreditCard CreditCard { get; set; }

        public int CreditCardId { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SubTotal", Justification = "This property is only used by Entity Framework.")]
        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}