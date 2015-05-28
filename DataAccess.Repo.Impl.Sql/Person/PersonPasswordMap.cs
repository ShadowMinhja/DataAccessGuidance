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
    
    public class PersonPasswordMap : EntityTypeConfiguration<PersonPassword>
    {
        public PersonPasswordMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("Password", "Person");

            // define the primary key for the table
            this.HasKey(p => p.BusinessEntityId);
        }
    }
}
