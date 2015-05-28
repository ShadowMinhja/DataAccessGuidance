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
    using System.Collections.Specialized;
    using System.Configuration.Fakes;
    using System.Net.Http;
    using System.Web.Http;
    using DataAccess.MvcWebApi;
    using DataAccess.MvcWebApi.Handlers;
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RouteConfigFixture
    {
        [TestMethod]
        public void ShouldRouteToAccountLogon()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/account/logon?username=johndoe&password=secret"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Account", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Logon", (string)routeData.Values["action"], true);
        }

        [TestMethod]
        public void ShouldRouteToAccountRegister()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Post, "http://foo/api/account/Register"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Account", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Register", (string)routeData.Values["action"], true);
        }

        // Cart routes
        [TestMethod]
        public void ShouldRouteToCartGet()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/cart/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        [TestMethod]
        public void ShouldRouteToCartAdd()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Post, "http://foo/api/cart/add"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Add", (string)routeData.Values["action"], true);
        }

        [TestMethod]
        public void ShouldRouteToCartSave()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Post, "http://foo/api/cart/save"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Save", (string)routeData.Values["action"], true);
        }

        [TestMethod]
        public void ShouldRouteToCartReplace()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Post, "http://foo/api/cart/replace"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Replace", (string)routeData.Values["action"], true);
        }

        [TestMethod]
        public void ShouldRouteToCartDeleteCartItem()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Delete, "http://foo/api/cart/deletecartitem"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("DeleteCartItem", (string)routeData.Values["action"], true);
        }

        [TestMethod]
        public void ShouldRouteToCartDelete()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Delete, "http://foo/api/cart/Delete"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Cart", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Delete", (string)routeData.Values["action"], true);
        }

        // Categories routes
        [TestMethod]
        public void ShouldRouteToCategoriesGetAll()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/categories"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Categories", (string)routeData.Values["controller"], true);
        }

        [TestMethod]
        public void ShouldRouteToCategoriesGet()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/categories/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Categories", (string)routeData.Values["controller"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        // Subcategories routes
        [TestMethod]
        public void ShouldRouteToSubcategoriesGetSubcategories()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/categories/1/subcategories"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/categories/{categoryId}/subcategories", routeData.Route.RouteTemplate);
            Assert.AreEqual("Subcategories", (string)routeData.Values["controller"], true);
            Assert.AreEqual("GetSubcategories", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["categoryId"], true);
        }

        [TestMethod]
        public void ShouldRouteToSubcategoriesGet()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/subcategories/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Subcategories", (string)routeData.Values["controller"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        // Products routes
        [TestMethod]
        public void ShouldRouteToProductsGetProducts()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/subcategories/1/products"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/subcategories/{subcategoryId}/products", routeData.Route.RouteTemplate);
            Assert.AreEqual("Products", (string)routeData.Values["controller"], true);
            Assert.AreEqual("GetProducts", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["subcategoryId"], true);
        }

        [TestMethod]
        public void ShouldRouteToProductsGet()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/products/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Products", (string)routeData.Values["controller"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        [TestMethod]
        public void ShouldRouteToProductsGetRecommendations()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/products/1/recommendations"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/products/{productId}/recommendations", routeData.Route.RouteTemplate);
            Assert.AreEqual("Products", (string)routeData.Values["controller"], true);
            Assert.AreEqual("GetRecommendations", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["productId"], true);
        }

        // Images routes
        [TestMethod]
        public void ShouldRouteToImagesGetCategoryImage()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/images/categories/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Images", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Categories", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        [TestMethod]
        public void ShouldRouteToImagesGetSubcategoryImage()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/images/subcategories/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Images", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Subcategories", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        [TestMethod]
        public void ShouldRouteToImagesGetProductImage()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/images/products/1"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Images", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Products", (string)routeData.Values["action"], true);
            Assert.AreEqual("1", (string)routeData.Values["id"], true);
        }

        // Order routes
        [TestMethod]
        public void ShouldRouteToOrderSave()
        {
            // Arrange
            var config = new HttpConfiguration();
            using (var context = ShimsContext.Create())
            {
                ShimConfigurationManager.AppSettingsGet = () => new NameValueCollection { { CorsHandler.CORSHandlerAllowerHostsSettings, "dummy" } };
                WebApiConfig.Register(config);
            }

            // Act
            var routeData = config.Routes.GetRouteData(new HttpRequestMessage(HttpMethod.Get, "http://foo/api/orders/save"));

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("api/{controller}/{action}/{id}", routeData.Route.RouteTemplate);
            Assert.AreEqual("Orders", (string)routeData.Values["controller"], true);
            Assert.AreEqual("Save", (string)routeData.Values["action"], true);
        }
    }
}
