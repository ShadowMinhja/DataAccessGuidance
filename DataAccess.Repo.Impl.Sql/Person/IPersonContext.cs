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
    using System;
    using System.Data.Entity;

    public interface IPersonContext : IDisposable
    {
        IDbSet<Person> Persons { get; set; }

        IDbSet<PersonEmailAddress> EmailAddresses { get; set; }

        IDbSet<PersonAddress> Addresses { get; set; }

        IDbSet<PersonCreditCard> CreditCards { get; set; }

        IDbSet<PersonBusinessEntity> BusinessEntities { get; set; }

        int SaveChanges();
    }
}