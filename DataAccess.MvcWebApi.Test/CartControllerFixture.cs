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
    using DataAccess.Domain.ShoppingCart;
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.MvcWebApi.Models;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class CartControllerFixture
    {
        [TestMethod]
        public void ShouldReturnCartItemsById()
        {
            // Arrange
            var shoppingCart = new ShoppingCart("1");
            shoppingCart.AddItem(new ShoppingCartItem()
            {
                ProductId = 1,
                ProductName = "Test Product",
                ProductPrice = 9.99m,
                Quantity = 3
            });

            var cartRepository = new StubIShoppingCartRepository
            {
                GetShoppingCartString = id => shoppingCart
            };
            var controller = new CartController(cartRepository, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get("1");
            var returnedCartItems = new JavaScriptSerializer().Deserialize<ICollection<CartItem>>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedCartItems.Count, shoppingCart.ShoppingCartItems.Count);
        }

        [TestMethod]
        public void ShouldReturnNotFoundForCartItemsById()
        {
            // Arrange
            var cartRepository = new StubIShoppingCartRepository() { GetShoppingCartString = id => null };
            var controller = new CartController(cartRepository, null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get("1");

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldAddAnItemToANewCart()
        {
            // Arrange
            var targetShoppingCartSaved = false;

            var cartRepository = new StubIShoppingCartRepository
            {
                SaveShoppingCartShoppingCart = (cart) =>
                {
                    targetShoppingCartSaved = true;
                    return cart;
                }
            };

            var productRepository = new StubIProductRepository
            {
                ProductExistsInt32 = productId => productId.Equals(1)
            };

            var controller = new CartController(cartRepository, productRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Add(new CartItem
            {
                ProductId = 1,
                ProductName = "Test Product",
                ProductPrice = 9.99m,
                Quantity = 1
            });

            // Assert
            Assert.IsTrue(targetShoppingCartSaved);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void ShoulReturnBadRequestWhenAddNullModel()
        {
            // Arrange
            var controller = new CartController(null, null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.Add(null);
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ShoulReturnBadRequestWhenAddInvalidProductId()
        {
            // Arrange
            var controller = new CartController(
                new StubIShoppingCartRepository(),
                new StubIProductRepository
                {
                    ProductExistsInt32 = productId => productId.Equals(1)
                });
            SetupControllerForTests(controller);

            // Act
            var response = controller.Add(new CartItem
            {
                ProductId = 2,
                ProductName = "some product",
                ProductPrice = 9.99m,
                Quantity = 1
            });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ShoulReturnBadRequestWhenAddInvalidItemModel()
        {
            // Arrange
            var controller = new CartController(
                new StubIShoppingCartRepository(),
                new StubIProductRepository
                {
                    ProductExistsInt32 = productId => productId.Equals(1)
                });
            SetupControllerForTests(controller);

            // Act
            var response = controller.Add(new CartItem
            {
                ProductId = 1,
                ProductName = string.Empty,  // invalid product name
                ProductPrice = 9.99m,
                Quantity = 1
            });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("The ProductName field is required.", error.Message);
        }

        [TestMethod]
        public void ShoulReturnInternalServerErrorWhenAddingWithException()
        {
            // Arrange
            var controller = new CartController(
                new StubIShoppingCartRepository
                {
                    SaveShoppingCartShoppingCart = cart =>
                    {
                        throw new Exception("error");
                    }
                },
                new StubIProductRepository
                {
                    ProductExistsInt32 = productId => productId.Equals(1)
                });
            SetupControllerForTests(controller);

            // Act
            var response = controller.Add(new CartItem
            {
                ProductId = 1,
                ProductName = "name",
                ProductPrice = 1m,
                Quantity = 1
            });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("error", error.Message);
        }

        [TestMethod]
        public void ShouldDeleteAnItem()
        {
            // Arrange
            var targetItemRemoved = false;

            var controller = new CartController(
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id => new ShoppingCart(id),
                    SaveShoppingCartShoppingCart = cart =>
                        {
                            targetItemRemoved = true;
                            return cart;
                        }
                },
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.DeleteCartItem(new CartItemDelete
            {
                ProductId = 11,
                ShoppingCartId = "id"
            });

            // Assert
            Assert.IsTrue(targetItemRemoved);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenDeletingItemFromUnexistentCart()
        {
            // Arrange
            var controller = new CartController(
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = (id) => null
                },
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.DeleteCartItem(new CartItemDelete
            {
                ProductId = 11,
                ShoppingCartId = "id"
            });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ShouldReturnInternalServerErrorWhenDeletingWithException()
        {
            // Arrange
            var controller = new CartController(
                new StubIShoppingCartRepository
                {
                    GetShoppingCartString = id => new ShoppingCart(id),
                    SaveShoppingCartShoppingCart = cart =>
                    {
                        throw new Exception("error");
                    }
                },
                null);
            SetupControllerForTests(controller);

            // Act
            var response = controller.DeleteCartItem(new CartItemDelete
            {
                ProductId = 11,
                ShoppingCartId = "id"
            });
            var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = string.Empty });

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("error", error.Message);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/cart");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "cart" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AutomapperConfig.SetAutoMapperConfiguration();
        }
    }
}
