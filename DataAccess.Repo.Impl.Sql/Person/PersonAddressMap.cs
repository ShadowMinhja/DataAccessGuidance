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
    using System.Data.Entity.ModelConfiguration;
    
    public class PersonAddressMap : EntityTypeConfiguration<PersonAddress>
    {
        public PersonAddressMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("Address", "Person");

            // define the primary key for the table
            this.HasKey(a => a.AddressId);
        }
    }
}
