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
    
    public class PersonCreditCardMap : EntityTypeConfiguration<PersonCreditCard>
    {
        public PersonCreditCardMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("CreditCard", "Sales");

            // define the primary key for the table
            this.HasKey(c => c.CreditCardId);

            // setup the many-to-many relationship between Person and PersonCreditCard
            this.HasMany(c => c.Persons)
                .WithMany(p => p.CreditCards)
                .Map(map => map.ToTable("PersonCreditCard", "Sales")
                    .MapLeftKey("BusinessEntityID")
                    .MapRightKey("CreditCardID"));
        }
    }
}
