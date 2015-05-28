//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Entities;
using DataAccess.Repo.Interface.Fakes;
using System.Collections.Generic;
using DataAccess.Storage.Impl.EF.Fakes;

namespace DataAccess.Repo.Impl.Sql.Test
{
    [TestClass]
    public class PersonRepositoryTest
    {
        [TestMethod]
        public void GetPerson_ReturnPerson()
        {
            // Arrange
            var person = new Person() { FirstName = "Test", LastName = "User" };
            var repository = new StubIPersonRepository() { GetPersonInt32 = personId => { return person; } };

            // Act
            var result = repository.GetPersonInt32(1);

            // Assert
            Assert.AreEqual(result, person);
        }

        [TestMethod]
        public void GetPerson_ReturnNull()
        {
            // Arrange
            var repository = new StubIPersonRepository() { GetPersonInt32 = personId => { return null; } };

            // Act
            var result = repository.GetPersonInt32(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPersonByEmail_ReturnPerson()
        {
            // Arrange
            var person = new Person 
            { 
                FirstName = "Test", 
                LastName = "User",
                EmailAddresses = new List<PersonEmailAddress>()
            };
            //person.EmailAddresses.Add(new PersonEmailAddress() { EmailAddress = "test@user.com" });

            var uow = new StubIDbContextUnitOfWork();
            var repository = new PersonRepository(uow);
            //var repository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => { return person; } };

            // Act
            var result = repository.GetPersonByEmail("test@user.com");

            // Assert
            Assert.AreEqual(result, person);
        }
    }
}
