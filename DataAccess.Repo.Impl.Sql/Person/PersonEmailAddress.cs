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
    public class PersonEmailAddress
    {
        public int BusinessEntityId { get; set; }

        public int EmailAddressId { get; set; }

        public string EmailAddress { get; set; }

        public virtual Person Person { get; set; }
    }
}