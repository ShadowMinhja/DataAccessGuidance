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
    using DataAccess.Domain.Person;
    using DataAccess.MvcWebApi.Controllers;
    using DataAccess.Repository.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class StatesControllerFixture
    {
        [TestMethod]
        public void ShouldGetStates()
        {
            // Arrange
            var controller = new StatesController(new StubIStateProvinceRepository
            {
                GetStateProvinces = () => new List<StateProvince> { new StateProvince { StateProvinceId = 1 } }
            });
            SetupControllerForTests(controller);

            // Act
            var result = controller.Get();
            var states = new JavaScriptSerializer().Deserialize<IEnumerable<StateProvince>>(result.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(1, states.Count());
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/states");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}
