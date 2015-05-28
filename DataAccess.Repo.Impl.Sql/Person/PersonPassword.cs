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
    public class PersonPassword
    {
        public int BusinessEntityId { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public virtual Person Person { get; set; }
    }
}