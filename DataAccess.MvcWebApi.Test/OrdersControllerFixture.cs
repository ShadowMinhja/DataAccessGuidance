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
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;
    using System.Web.Script.Serialization;
    using DataAccess.Domain.Catalog;
    using DataAccess.Domain.Order;
    using DataAccess.Domain.Person;
    using DataAccess.Domain.Services.Interface.Fakes;
    using DataAccess.Domain.ShoppingCart;
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.MvcWebApi.Models;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class OrdersControllerFixture
    {
        [TestMethod]
        public void ShouldGetOrderByTrackingId()
        {
            // Arrange
            var targetTrackingId = Guid.NewGuid();

            var controller = new OrdersController(
                new StubIOrderHistoryRepository
                {
                    GetOrderHistoryByTrackingIdGuid = id => id.Equals(targetTrackingId) ? new OrderHistory { TrackingId = id } : null
                },
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id => new ShoppingCart(id) { TrackingId = Guid.NewGuid() }
                },
                null,
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Get(targetTrackingId.ToString());
            var order = new JavaScriptSerializer().Deserialize<OrderDetail>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.IsNotNull(order);
            Assert.AreEqual(targetTrackingId, order.TrackingId);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenInvalidTrackingId()
        {
            // Arrange
            var targetTrackingId = Guid.NewGuid();

            var controller = new OrdersController(
                new StubIOrderHistoryRepository
                {
                    GetOrderHistoryByTrackingIdGuid = id => id.Equals(targetTrackingId) ? new OrderHistory { TrackingId = id } : null
                },
                null,
                null,
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Get(Guid.NewGuid().ToString());
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ShouldGetOrdersHistoryForCustomer()
        {
            // Arrange
            var targetTrackingId = Guid.NewGuid();
            var personGuid = Guid.NewGuid();

            var controller = new OrdersController(
                new StubIOrderHistoryRepository
                {
                    GetOrdersHistoriesInt32 = id => id.Equals(101)
                        ? new List<OrderHistory> 
                        {
                            new OrderHistory()
                        }
                        : null
                },
                null,
                new StubIPersonRepository
                {
                    GetPersonGuid = id => id.Equals(personGuid) ? new Person { Id = 101 } : null
                },
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.GetHistory(personGuid.ToString());
            var orders = new JavaScriptSerializer().Deserialize<IEnumerable<OrderHistoryInfo>>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.IsNotNull(orders);
            Assert.AreEqual(1, orders.Count());
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenOrdersHistoryForInvalidCustomer()
        {
            // Arrange
            var targetTrackingId = Guid.NewGuid();
            var personGuid = Guid.NewGuid();

            var controller = new OrdersController(
                null,
                null,
                new StubIPersonRepository
                {
                    GetPersonGuid = id => id.Equals(personGuid) ? new Person { PersonGuid = personGuid } : null
                },
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.GetHistory(Guid.NewGuid().ToString());
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ShouldGetOrderHistoryDetails()
        {
            // Arrange
            var targetHistoryId = Guid.NewGuid();

            var controller = new OrdersController(
                new StubIOrderHistoryRepository
                {
                    GetOrderHistoryByHistoryIdGuid = id => id.Equals(targetHistoryId) ? new OrderHistory { HistoryId = id } : null
                },
                null,
                null,
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.GetHistoryDetails(targetHistoryId.ToString());
            var orderDetail = new JavaScriptSerializer().Deserialize<OrderHistoryInfo>(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.IsNotNull(orderDetail);
            Assert.AreEqual(targetHistoryId, orderDetail.HistoryId);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenInvalidHistoryId()
        {
            // Arrange
            var targetHistoryId = Guid.NewGuid();

            var controller = new OrdersController(
                new StubIOrderHistoryRepository
                {
                    GetOrderHistoryByHistoryIdGuid = id => id.Equals(targetHistoryId) ? new OrderHistory { HistoryId = id } : null
                },
                null,
                null,
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.GetHistoryDetails(Guid.NewGuid().ToString());
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ShouldSaveOrder()
        {
            // Arrange
            var personGuid = Guid.NewGuid();

            var orderInfo = new OrderInfo()
            {
                BillingAddressId = 1,
                CartId = personGuid.ToString(),
                CreditCardId = 1,
                ShippingAddressId = 1
            };

            var person = new Person() { Id = 1 };
            person.AddAddress(new Address
            {
                Id = 1,
                AddressLine1 = "street 1234",
                City = "city",
                PostalCode = "1001"
            });
            person.AddCreditCard(new CreditCard
            {
                Id = 1,
                CardType = "card",
                CardNumber = "1234567890123456",
                ExpMonth = 11,
                ExpYear = 2019
            });

            var address = new Address() { Id = 1 };
            var creditCard = new CreditCard() { Id = 1 };

            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product",
                ProductPrice = 1,
                Quantity = 1
            };

            var shoppingCart = new ShoppingCart("1");
            shoppingCart.AddItem(shoppingCartItem);

            var inventoryService = new StubIInventoryService
            {
                InventoryAndPriceCheckShoppingCart = shoppingCartToCheck => false
            };

            var personRepository = new StubIPersonRepository
            {
                GetPersonGuid = personId => person,
            };

            var shoppingCartRepository = new StubIShoppingCartRepository
            {
                GetShoppingCartString = shoppingCartId => shoppingCart
            };

            var productRepository = new StubIProductRepository
            {
                GetProductInt32 = productId => new Product() { Id = productId }
            };

            var controller = new OrdersController(
                new StubIOrderHistoryRepository(),
                shoppingCartRepository,
                personRepository,
                productRepository,
                new StubIOrderService(),
                inventoryService);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(orderInfo);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenSavingEmptyOrder()
        {
            // Arrange
            var controller = new OrdersController(null, null, null, null, null, null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(null);

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithInvalidPerson()
        {
            // Arrange
            var controller = new OrdersController(null, null, new StubIPersonRepository(), null, null, null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString() });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithInvalidCart()
        {
            // Arrange
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository(),
                new StubIPersonRepository
                {
                    GetPersonGuid = (id) => new Person()
                },
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString() });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithEmptyCart()
        {
            // Arrange
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = (id) => new ShoppingCart(id)
                },
                new StubIPersonRepository
                {
                    GetPersonGuid = (id) => new Person()
                },
                null,
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString() });

            // Assert
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithInvalidShippingAddress()
        {
            // Arrange
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id => 
                    {
                        var cart = new ShoppingCart(id);
                        cart.AddItem(new ShoppingCartItem { ProductName = "name", ProductPrice = 1m, Quantity = 1 });
                        return cart;
                    }
                },
                new StubIPersonRepository
                {
                    GetPersonGuid = id => new Person()
                },
                new StubIProductRepository
                {
                    GetProductInt32 = id => new Product()
                },
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString(), ShippingAddressId = 10 });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithInvalidBillingAddress()
        {
            // Arrange
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id =>
                    {
                        var cart = new ShoppingCart(id);
                        cart.AddItem(new ShoppingCartItem { ProductName = "name", ProductPrice = 1m, Quantity = 1 });
                        return cart;
                    }
                },
                new StubIPersonRepository
                {
                    GetPersonGuid = id =>
                    {
                        var person = new Person();
                        person.AddAddress(new Address { Id = 13, AddressLine1 = "street", City = "city", PostalCode = "pcode" });
                        return person;
                    }
                },
                new StubIProductRepository
                {
                    GetProductInt32 = id => new Product()
                },
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString(), ShippingAddressId = 13, BillingAddressId = 17 });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenSaveOrderWithInvalidCreditCard()
        {
            // Arrange
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id =>
                    {
                        var cart = new ShoppingCart(id);
                        cart.AddItem(new ShoppingCartItem { ProductName = "name", ProductPrice = 1m, Quantity = 1 });
                        return cart;
                    }
                },
                new StubIPersonRepository
                {
                    GetPersonGuid = id =>
                    {
                        var person = new Person();
                        person.AddAddress(new Address { Id = 13, AddressLine1 = "street", City = "city", PostalCode = "pcode" });
                        return person;
                    }
                },
                new StubIProductRepository
                {
                    GetProductInt32 = id => new Product()
                },
                null,
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString(), ShippingAddressId = 13, BillingAddressId = 13, CreditCardId = 29 });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnOkAndSaveWhenSaveOrderWithInventoryChecking()
        {
            // Arrange
            var targetShoppingCartSaved = false;
            var controller = new OrdersController(
                null,
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id =>
                    {
                        var cart = new ShoppingCart(id);
                        cart.AddItem(new ShoppingCartItem { ProductName = "name", ProductPrice = 1m, Quantity = 1 });
                        return cart;
                    },
                    SaveShoppingCartShoppingCart = cart =>
                    {
                        targetShoppingCartSaved = true;
                        return cart;
                    }
                },
                new StubIPersonRepository
                {
                    GetPersonGuid = id =>
                    {
                        var person = new Person();
                        person.AddAddress(new Address { Id = 13, AddressLine1 = "street", City = "city", PostalCode = "pcode" });
                        person.AddCreditCard(new CreditCard { Id = 29, CardNumber = "1234567890123456", CardType = "type", ExpMonth = 11, ExpYear = 2019 });
                        return person;
                    }
                },
                new StubIProductRepository
                {
                    GetProductInt32 = id => new Product()
                },
                null,
                new StubIInventoryService
                {
                    InventoryAndPriceCheckShoppingCart = cart => true
                });
            SetupControllerForTests(controller);

            // Act
            var response = controller.Post(new OrderInfo { CartId = Guid.NewGuid().ToString(), ShippingAddressId = 13, BillingAddressId = 13, CreditCardId = 29 });

            // Assert
            Assert.IsTrue(targetShoppingCartSaved);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/orders");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "orders" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AutomapperConfig.SetAutoMapperConfiguration();
        }
    }
}
