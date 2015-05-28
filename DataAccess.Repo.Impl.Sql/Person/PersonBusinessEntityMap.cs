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
    
    public class PersonBusinessEntityMap : EntityTypeConfiguration<PersonBusinessEntity>
    {
        public PersonBusinessEntityMap()
            : base()
        {
            this.ToTable("BusinessEntity", "Person");
            this.HasKey(b => b.BusinessEntityId);
        }
    }
}
