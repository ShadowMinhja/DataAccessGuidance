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

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("Person", "Person");

            // define the primary key for the table
            this.HasKey(p => p.BusinessEntityId);

            // setup the required relationship between Person and Password
            this.HasRequired(p => p.Password)
                .WithRequiredPrincipal(p => p.Person)
                .WillCascadeOnDelete(true);

            // setup the required relationship between Person and EmailAddress
            this.HasMany(p => p.EmailAddresses)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(true);

            // setup the many-to-many relationship between Person and PersonCreditCard
            this.HasMany(p => p.CreditCards)
                .WithMany(c => c.Persons)
                .Map(map => map.ToTable("PersonCreditCard", "Sales")
                    .MapLeftKey("BusinessEntityID")
                    .MapRightKey("CreditCardID"));
        }
    }
}
