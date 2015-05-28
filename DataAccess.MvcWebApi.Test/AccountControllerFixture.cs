//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.MvcWebApi.Test
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;
    using System.Web.Script.Serialization;
    using DataAccess.Domain.Person;
    using DataAccess.Domain.ShoppingCart;
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.MvcWebApi.Models;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class AccountControllerFixture
    {
        [TestMethod]
        public void ShouldGetPerson()
        {
            // Arrange
            var personGuid = Guid.NewGuid();
            var person = new Person { FirstName = "Test", LastName = "User", PersonGuid = personGuid };
            person.AddAddress(new Address
            {
                AddressLine1 = "street",
                City = "city",
                PostalCode = "pcode",
                StateProvinceId = 1
            });
            person.AddCreditCard(new CreditCard
            {
                CardType = "type",
                CardNumber = "1234567890123456",
                ExpMonth = 11,
                ExpYear = 2019
            });
            person.AddEmailAddress("person@mail.org");

            var stateProvinces = new List<StateProvince>();
            stateProvinces.Add(new StateProvince() { StateProvinceId = 1, StateProvinceCode = "WA" });

            var repository = new StubIPersonRepository { GetPersonGuid = id => id.Equals(personGuid) ? person : null };
            var stateProvinceRepository = new StubIStateProvinceRepository { GetStateProvinces = () => stateProvinces };

            var controller = new AccountController(repository, null, null, stateProvinceRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get(personGuid.ToString());
            var personDetail = new JavaScriptSerializer().Deserialize<PersonDetail>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual("Test", personDetail.FirstName);
            Assert.AreEqual("User", personDetail.LastName);
            Assert.AreEqual(1, personDetail.Addresses.Count);
            Assert.AreEqual(1, personDetail.CreditCards.Count);
            Assert.AreEqual(1, personDetail.EmailAddresses.Count);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenGetInvalidPerson()
        {
            // Arrange
            var personGuid = Guid.NewGuid();
            var person = new Person { FirstName = "Test", LastName = "User", PersonGuid = personGuid };

            var repository = new StubIPersonRepository { GetPersonGuid = id => id.Equals(personGuid) ? person : null };
            var controller = new AccountController(repository, null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var response = controller.Get(Guid.NewGuid().ToString());
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ShouldLogonUser()
        {
            // Arrange
            var person = new Person()
            {
                Id = 1,
                PasswordHash = "/y5qFg+Su7P2S+iiVMKgSHzlSemQan2hYvZywrqSkTE=",
                PasswordSalt = "bE3XiWw=",
                PersonGuid = Guid.NewGuid()
            };

            var logonInfo = new LoginInfo()
            {
                UserName = "ken0@adventure-works.com",
                Password = "ken0@adventure-works.com"
            };

            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };
            var shoppingCartRepository = new StubIShoppingCartRepository() { GetShoppingCartString = shoppingCartId => new ShoppingCart(person.PersonGuid.ToString()) };
            var orderRepository = new StubISalesOrderRepository() { IsOrderSavedGuid = shoppingCartId => false };
            var controller = new AccountController(personRepository, shoppingCartRepository, orderRepository, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Login(logonInfo);
            var returnedPerson = new JavaScriptSerializer().Deserialize<string>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedPerson, person.PersonGuid.ToString());
        }

        [TestMethod]
        public void ShouldReturnNotFoundPersonForLogon()
        {
            // Arrange
            var repository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => null };
            var controller = new AccountController(repository, null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Login(new LoginInfo());

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldFailAuthenticationForLogon()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                PasswordHash = "IncorrectHash",
                PasswordSalt = "bE3XiWw="
            };

            var logonInfo = new LoginInfo()
            {
                UserName = "user@test.com",
                Password = "user@test.com"
            };

            var repository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };
            var controller = new AccountController(repository, null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Login(logonInfo);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldRegisterUser()
        {
            // Arrange
            var registrationInfo = new RegistrationInfo()
            {
                Addresses = new List<AddressInfo>()
                {
                    new AddressInfo() { AddressLine1 = "123", City = "Redmond", CountryName = "USA", PostalCode = "12345", StateProvinceId = 33 }
                },
                CreditCards = new List<CreditCardInfo>()
                {
                    new CreditCardInfo() { CardNumber = "1111222233334444", CardType = "VISA", ExpMonth = 1, ExpYear = 2015 }
                },
                EmailAddresses = new List<string>() { "test@user.com" },
                FirstName = "Test",
                LastName = "User",
                Password = "password"
            };

            var person = new Person() { PersonGuid = Guid.NewGuid() };

            var personRepository = new StubIPersonRepository() { SavePersonPerson = newPerson => person };
            var shoppingCartRepository = new StubIShoppingCartRepository() { GetShoppingCartString = shoppingCartId => new ShoppingCart(person.PersonGuid.ToString()) };
            var orderRepository = new StubISalesOrderRepository() { IsOrderSavedGuid = shoppingCartId => false };
            var controller = new AccountController(personRepository, shoppingCartRepository, orderRepository, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(registrationInfo);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyBodyRequest()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(null);

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyName()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo());

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyPassword()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe"
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyEmails()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret"
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyAddresses()
        {
            // Arrange
            var controller = new AccountController(null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithEmptyCreditCards()
        {
            // Arrange
            var controller = new AccountController(null, null,null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> { new AddressInfo() }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidAddresses()
        {
            // Arrange
            var person = new Person();
            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };

            var controller = new AccountController(personRepository, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> { new AddressInfo() },
                CreditCards = new List<CreditCardInfo> { new CreditCardInfo() }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidCreditCard()
        {
            // Arrange
            var person = new Person();
            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };

            var controller = new AccountController(personRepository, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo> { new CreditCardInfo() }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidCreditCardNumber()
        {
            // Arrange
            var person = new Person();
            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };

            var controller = new AccountController(personRepository, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "123454321",
                        ExpMonth = 11,
                        ExpYear = 19
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidExpMonth()
        {
            // Arrange
            var person = new Person();
            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };

            var controller = new AccountController(personRepository, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "1234567890123456",
                        ExpMonth = 71,
                        ExpYear = 19
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidExpYear()
        {
            // Arrange
            var person = new Person();
            var personRepository = new StubIPersonRepository() { GetPersonByEmailString = emailAddress => person };

            var controller = new AccountController(personRepository, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "john.doe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "1234567890123456",
                        ExpMonth = 11,
                        ExpYear = 5
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithInvalidEmailAddress()
        {
            // Arrange
            var repository = new StubIPersonRepository() { GetPersonByEmailString = email => new Person() };
            var controller = new AccountController(repository, null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "johndoe" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "1234567890123456",
                        ExpMonth = 11,
                        ExpYear = 19
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringWithRegisteredEmailAddress()
        {
            // Arrange
            var controller = new AccountController(
                new StubIPersonRepository
                {
                    GetPersonByEmailString = (email) => new Person()
                },
                null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "johndoe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "1234567890123456",
                        ExpMonth = 11,
                        ExpYear = 19
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenRegisteringAndFailOnSaving()
        {
            // Arrange
            var controller = new AccountController(
                new StubIPersonRepository
                {
                    GetPersonByEmailString = (email) => null,
                    SavePersonPerson = (person) =>
                    {
                        throw new Exception("failed on save");
                    }
                },
                null, null, new StubIStateProvinceRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.Register(new RegistrationInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Password = "secret",
                EmailAddresses = new List<string> { "johndoe@mail.org" },
                Addresses = new List<AddressInfo> 
                { 
                    new AddressInfo 
                    { 
                        AddressLine1 = "X", 
                        City = "Y", 
                        PostalCode = "Z",
                        StateProvinceId = 1
                    }
                },
                CreditCards = new List<CreditCardInfo>
                {
                    new CreditCardInfo
                    {
                        CardType = "M",
                        CardNumber = "1234567890123456",
                        ExpMonth = 11,
                        ExpYear = 19
                    }
                }
            });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(
                result.Content.ReadAsStringAsync().Result,
                new { Message = string.Empty });
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/account");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "account" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AutomapperConfig.SetAutoMapperConfiguration();
        }
    }
}
