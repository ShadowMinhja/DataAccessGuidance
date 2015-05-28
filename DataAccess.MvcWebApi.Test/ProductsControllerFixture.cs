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
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.MvcWebApi.Models;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class ProductsControllerFixture
    {
        [TestMethod]
        public void ShouldReturnProductsForASubcategory()
        {
            // Arrange
            var targetProducts = new List<Product>
            {
                new Product() { Id = 1 },
                new Product() { Id = 2, Subcategory = new DataAccess.Domain.Catalog.Subcategory() },
                new Product() { Id = 3, Subcategory = new DataAccess.Domain.Catalog.Subcategory { Category = new Domain.Catalog.Category() } }
            };

            var controller = new ProductsController(
                new StubIProductRepository() { GetProductsInt32 = subcategoryId => targetProducts }, 
                null);
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetProducts(1);
            var returnedProducts = new JavaScriptSerializer().Deserialize<ICollection<ProductInfo>>(result.Content.ReadAsStringAsync().Result);
            
            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedProducts.Count, targetProducts.Count);
        }

        [TestMethod]
        public void ShouldReturnNotFoundForProducts()
        {
            // Arrange
            var productRepository = new StubIProductRepository() { GetProductsInt32 = subcategoryId => null };
            var recommendationRepository = new StubIProductRecommendationRepository();
            var controller = new ProductsController(productRepository, recommendationRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetProducts(1);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldReturnProduct()
        {
            // Arrange
            var product = new Product()
            {
                Class = "TestClass",
                Color = "TestColor",
                ListPrice = 9.99m,
                Name = "TestProduct",
                Id = 1,
                ProductNumber = "TestProductNumber",
                Size = "TestSize",
                SizeUnitMeasureCode = "FT",
                Style = "TestStyle",
                Weight = 2,
                WeightUnitMeasureCode = "LBS",
                Subcategory = new Domain.Catalog.Subcategory { Category = new Domain.Catalog.Category() }
            };

            var repository = new StubIProductRepository() { GetProductInt32 = productId => product };
            var recommendationRepository = new StubIProductRecommendationRepository();
            var controller = new ProductsController(repository, recommendationRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get(1);
            var returnedProduct = new JavaScriptSerializer().Deserialize<ProductDetail>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedProduct.Id, product.Id);
        }

        [TestMethod]
        public void ShouldReturnProductWithoutSubcategory()
        {
            // Arrange
            var product = new Product()
            {
                Class = "TestClass",
                Color = "TestColor",
                ListPrice = 9.99m,
                Name = "TestProduct",
                Id = 1,
                ProductNumber = "TestProductNumber",
                Size = "TestSize",
                SizeUnitMeasureCode = "FT",
                Style = "TestStyle",
                Weight = 2,
                WeightUnitMeasureCode = "LBS",
            };

            var repository = new StubIProductRepository() { GetProductInt32 = productId => product };
            var recommendationRepository = new StubIProductRecommendationRepository();
            var controller = new ProductsController(repository, recommendationRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get(1);
            var returnedProduct = new JavaScriptSerializer().Deserialize<ProductDetail>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedProduct.Id, product.Id);
        }

        [TestMethod]
        public void ShouldReturnProductWithoutCategory()
        {
            // Arrange
            var product = new Product()
            {
                Class = "TestClass",
                Color = "TestColor",
                ListPrice = 9.99m,
                Name = "TestProduct",
                Id = 1,
                ProductNumber = "TestProductNumber",
                Size = "TestSize",
                SizeUnitMeasureCode = "FT",
                Style = "TestStyle",
                Weight = 2,
                WeightUnitMeasureCode = "LBS",
                Subcategory = new Domain.Catalog.Subcategory()
            };

            var repository = new StubIProductRepository() { GetProductInt32 = productId => product };
            var recommendationRepository = new StubIProductRecommendationRepository();
            var controller = new ProductsController(repository, recommendationRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get(1);
            var returnedProduct = new JavaScriptSerializer().Deserialize<ProductDetail>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedProduct.Id, product.Id);
        }

        [TestMethod]
        public void ShouldReturnNotFoundForOneProduct()
        {
            // Arrange
            var repository = new StubIProductRepository() { GetProductInt32 = productId => null };
            var recommendationRepository = new StubIProductRecommendationRepository();
            var controller = new ProductsController(repository, recommendationRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldGetRecomendations()
        {
            // Arrange
            var controller = new ProductsController(
                new StubIProductRepository(),
                new StubIProductRecommendationRepository
                {
                    GetProductRecommendationsInt32 = (id) => new List<RecommendedProduct>
                    {
                        new RecommendedProduct { ProductId = 2 },
                        new RecommendedProduct { ProductId = 3 }
                    }
                });
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetRecommendations(1);
            var recommendations = new JavaScriptSerializer().Deserialize<IEnumerable<Recommendation>>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(2, recommendations.Count());
        }

        [TestMethod]
        public void ShouldReturnNotFoundGetRecomendationsForInvalidProduct()
        {
            // Arrange
            var controller = new ProductsController(
                new StubIProductRepository(),
                new StubIProductRecommendationRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetRecommendations(1);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenNoRecomendations()
        {
            // Arrange
            var controller = new ProductsController(
                new StubIProductRepository
                {
                    GetProductInt32 = (id) => new Product()
                },
                new StubIProductRecommendationRepository());
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetRecommendations(1);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}
