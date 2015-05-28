//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.StateProvince
{
    using System.Data.Entity.ModelConfiguration;
    
    public class PersonStateProvinceMap : EntityTypeConfiguration<StateProvince>
    {
        public PersonStateProvinceMap()
            : base()
        {
            // setup the schema.table mapping
            this.ToTable("StateProvince", "Person");

            // define the primary key for the table
            this.HasKey(s => s.StateProvinceId);
        }
    }
}
