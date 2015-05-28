//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.Person
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class Person : PersonBusinessEntity
    {
        public string PersonType { get; set; }

        public bool NameStyle { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public Guid PersonGuid { get; set; }

        public virtual PersonPassword Password { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<PersonEmailAddress> EmailAddresses { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<PersonBusinessEntityAddress> Addresses { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<PersonCreditCard> CreditCards { get; set; }
    }
}