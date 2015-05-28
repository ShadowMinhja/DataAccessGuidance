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

    public class PersonAddress
    {
        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public int StateProvinceId { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property is only used by Entity Framework.")]
        public virtual ICollection<PersonBusinessEntityAddress> Persons { get; set; }
    }
}