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
    public class SubcategoriesControllerFixture
    {
        [TestMethod]
        public void ShouldReturnSubcategoriesForCategoryId()
        {
            // Arrange
            var category = new Category { Name = "TestCategory", Id = 1 };
            var categoryRepository = new StubICategoryRepository
            {
                GetSubcategoriesInt32 = categoryId => new List<Subcategory>
                {
                    new Subcategory() { Name = "SubcategoryTest1", Id = 1, Category = category },
                    new Subcategory() { Name = "SubcategoryTest2", Id = 2, Category = category }
                }
            };
            var controller = new SubcategoriesController(categoryRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetSubcategories(1);
            var returnedSubcategories = new JavaScriptSerializer().Deserialize<ICollection<Subcategory>>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(2, returnedSubcategories.Count);
        }

        [TestMethod]
        public void ShouldReturnNotFoundForSubcategories()
        {
            // Arrange
            var categoryRepository = new StubICategoryRepository { GetSubcategoriesInt32 = categoryId => null };
            var controller = new SubcategoriesController(categoryRepository);
            SetupControllerForTests(controller);

            // Act
            var result = controller.GetSubcategories(1);

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/subcategories");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "subcategories" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}
