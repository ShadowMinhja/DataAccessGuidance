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
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "We need IDisposable at context's interface but base class implements it")]
    public sealed class PersonContext : BaseContext<PersonContext>, IPersonContext
    {
        public IDbSet<Person> Persons { get; set; }

        public IDbSet<PersonEmailAddress> EmailAddresses { get; set; }

        public IDbSet<PersonAddress> Addresses { get; set; }

        public IDbSet<PersonCreditCard> CreditCards { get; set; }

        public IDbSet<PersonBusinessEntity> BusinessEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new PersonBusinessEntityMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PersonEmailAddressMap());
            modelBuilder.Configurations.Add(new PersonAddressMap());
            modelBuilder.Configurations.Add(new PersonCreditCardMap());
            modelBuilder.Configurations.Add(new PersonPasswordMap());
            modelBuilder.Configurations.Add(new PersonBusinessEntityAddressMap());
        }
    }
}