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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class PersonCreditCard
    {
        public int CreditCardId { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<Person> Persons { get; set; }
    }
}