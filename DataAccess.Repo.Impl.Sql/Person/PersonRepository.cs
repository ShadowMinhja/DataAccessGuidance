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
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using DataAccess.Repo.Impl.Sql.Resources;
    using DataAccess.Repository;
    using DE = DataAccess.Domain.Person;

    public class PersonRepository : BaseRepository, IPersonRepository
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This method could potentially be refactored to remove some of the coupling.")]
        public DE.Person GetPerson(Guid personIdentifier)
        {
            try
            {
                using (var context = new PersonContext())
                {
                    Person person = null;

                    using (var transactionScope = this.GetTransactionScope())
                    {
                        person = context.Persons
                            .Include(p => p.Addresses)
                            .Include(p => p.CreditCards)
                            .Include(p => p.EmailAddresses)
                            .Include(p => p.Password)
                            .SingleOrDefault(p => p.PersonGuid == personIdentifier);

                        transactionScope.Complete();
                    }

                    if (person == null)
                    {
                        return null;
                    }

                    var result = new DE.Person();
                    var addresses = new List<DE.Address>();
                    var creditCards = new List<DE.CreditCard>();

                    Mapper.Map(person.Addresses, addresses);
                    Mapper.Map(person.CreditCards, creditCards);
                    Mapper.Map(person, result);

                    addresses.ForEach(a => result.AddAddress(a));
                    creditCards.ForEach(c => result.AddCreditCard(c));
                    person.EmailAddresses.ToList().ForEach(e => result.AddEmailAddress(e.EmailAddress));

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingPersonByIdentifier, personIdentifier),
                    e);
            }
        }

        public DE.Person GetPersonByEmail(string emailAddress)
        {
            try
            {
                using (var context = new PersonContext())
                {
                    PersonEmailAddress personEmail = null;
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        personEmail = context.EmailAddresses
                            .Include(pe => pe.Person)
                            .FirstOrDefault(ea => ea.EmailAddress.Equals(emailAddress));

                        transactionScope.Complete();
                    }

                    if (personEmail == null)
                    {
                        return null;
                    }

                    var result = new DE.Person();
                    Mapper.Map(personEmail.Person, result);

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingPersonByEmailAddress, emailAddress),
                    e);
            }
        }

        public DE.Person SavePerson(DE.Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            try
            {
                var newPerson = new Person()
        {
            Addresses = new List<PersonBusinessEntityAddress>(),
            CreditCards = new List<PersonCreditCard>(),
            EmailAddresses = new List<PersonEmailAddress>(),
            Password = new PersonPassword()
        };

                Mapper.Map(person, newPerson);

                // email addresses don't map neatly so we'll add them manually
                foreach (var emailAddress in person.EmailAddresses)
                {
                    newPerson.EmailAddresses.Add(new PersonEmailAddress() { EmailAddress = emailAddress });
                }

                // addresses don't map neatly so we'll add them manually
                foreach (var address in person.Addresses)
                {
                    var personAddress = new PersonAddress();
                    Mapper.Map(address, personAddress);

                    newPerson.Addresses.Add(new PersonBusinessEntityAddress()
                    {
                        Address = personAddress,
                        AddressTypeId = 2 // static value
                    });
                }

                // since the PersonGuid is a storage implementation, we create it now instead of earlier when the DE was created
                newPerson.PersonGuid = Guid.NewGuid();

                try
                {
                    using (var context = new PersonContext())
                    {
                        using (var transactionScope = this.GetTransactionScope())
                        {
                            context.Persons.Add(newPerson);
                            context.SaveChanges();

                            // assign the PK from the DB to the person object
                            person.Id = newPerson.BusinessEntityId;

                            transactionScope.Complete();
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    var sb = new StringBuilder();
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is PersonCreditCard
                            && ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate")
                            && ex.InnerException.InnerException.Message.Contains("Sales.CreditCard"))
                        {
                            sb.AppendFormat("The credit card is already registered: {0}\n", (entry.Entity as PersonCreditCard).CardNumber);
                        }
                        else if (entry.Entity is PersonAddress
                            && ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate")
                            && ex.InnerException.InnerException.Message.Contains("Person.Address"))
                        {
                            var personAddress = entry.Entity as PersonAddress;
                            sb.AppendFormat(
                                "The address is already registered: {0}, {1}, ({2})\n",
                                personAddress.AddressLine1,
                                personAddress.City,
                                personAddress.PostalCode);
                        }
                    }

                    throw new DbUpdateException(sb.ToString(), ex);
                }

                return person;
            }
            catch (Exception e)
            {
                throw new RepositoryException(Strings.ErrorSavingPerson, e);
            }
        }

        public bool IsCreditCardRegistered(string creditCardNumber)
        {
            try
            {
                PersonCreditCard pc = null;
                using (var context = new PersonContext())
                {
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        pc = context.CreditCards
                            .FirstOrDefault(c => c.CardNumber.Equals(creditCardNumber));

                        transactionScope.Complete();
                    }

                    if (pc != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException(Strings.ErrorCheckingCreditCardRegistration, e);
            }

            return false;
        }
    }
}
