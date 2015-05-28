//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.DomainEntities.Test
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataAccess.Domain.Person;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class PersonFixture
    {
        [TestMethod]
        public void ShouldAddAnAddressToPerson()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var address = new Address()
            {
                AddressLine1 = "AddressLine1",
                City = "City",
                Id = 1,
                PostalCode = "PostalCode",
                StateProvinceId = 3
            };

            // Act
            person.AddAddress(address);

            // Assert
            Assert.AreEqual(address, person.Addresses.FirstOrDefault(a => a.Id == address.Id));
        }

        [TestMethod]
        public void ShouldAddACreditCardToPerson()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var creditCard = new CreditCard()
            {
                CardNumber = "4444555566667777",
                CardType = "Visa",
                ExpMonth = 10,
                ExpYear = 2018,
                Id = 1
            };

            // Act
            person.AddCreditCard(creditCard);

            // Assert
            Assert.AreEqual(creditCard, person.CreditCards.FirstOrDefault(c => c.Id == creditCard.Id));
        }

        [TestMethod]
        public void ShouldAddAnEmailAddressToPerson()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var emailAddress = "test@test.com";

            // Act
            person.AddEmailAddress(emailAddress);

            // Assert
            Assert.AreEqual(emailAddress, person.EmailAddresses.FirstOrDefault(e => e == emailAddress));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ShouldThrowValidationExceptionWhenAddingAddress()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var address = new Address()
            {
                AddressLine1 = string.Empty,
                City = string.Empty,
                Id = 1,
                PostalCode = null,
                StateProvinceId = 3
            };

            // Act
            person.AddAddress(address);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ShouldThrowValidationExceptionWhenAddingCreditCard()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var creditCard = new CreditCard()
            {
                CardNumber = "abc",
                CardType = string.Empty,
                ExpMonth = 10,
                ExpYear = 18,
                Id = 1
            };

            // Act
            person.AddCreditCard(creditCard);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ShouldThrowValidationExceptionWhenAddingEmailAddress()
        {
            // Arrange
            var person = new Person()
            {
                FirstName = "Test",
                Id = 1,
                LastName = "User",
                PasswordHash = "passworHash",
                PasswordSalt = "passwordSalt"
            };

            var emailAddress = "test";

            // Act
            person.AddEmailAddress(emailAddress);
        }
    }
}
