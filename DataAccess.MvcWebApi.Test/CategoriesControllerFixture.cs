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
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;
    using System.Web.Script.Serialization;
    using DataAccess.Domain.Catalog;
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class CategoriesControllerFixture
    {
        [TestMethod]
        public void ShouldReturnAllCategories()
        {
            // Arrange
            var productCategories = new List<Category>();
            productCategories.Add(new Category() { Name = "TestCategory1", Id = 1 });
            productCategories.Add(new Category() { Name = "TestCategory2", Id = 2 });
            
            var repository = new StubICategoryRepository() { GetAllCategories = () => productCategories };
            var controller = new CategoriesController(repository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetAll();
            var returnedCategories = new JavaScriptSerializer().Deserialize<ICollection<Category>>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(returnedCategories.Count, productCategories.Count);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/categories");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "categories" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AutomapperConfig.SetAutoMapperConfiguration();
        }
    }
}
