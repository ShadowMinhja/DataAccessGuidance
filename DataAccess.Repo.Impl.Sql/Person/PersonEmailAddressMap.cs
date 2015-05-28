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
    
    public class PersonEmailAddressMap : EntityTypeConfiguration<PersonEmailAddress>
    {
        public PersonEmailAddressMap()
            : base()
        {
            this.ToTable("EmailAddress", "Person");
            this.HasKey(e => e.EmailAddressId);

            this.HasRequired(e => e.Person)
                .WithMany()
                .WillCascadeOnDelete(true);
        }
    }
}
