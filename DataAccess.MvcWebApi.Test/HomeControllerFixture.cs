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
    using System.Web.Mvc;
    using DataAccess.MvcWebApi.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class HomeControllerFixture
    {
        [TestMethod]
        public void ShouldReturnSiteHomeView()
        {
            // Act
            var view = new HomeController().Index();

            // Assert
            Assert.IsInstanceOfType(view, typeof(ViewResult));
            Assert.IsTrue(string.IsNullOrWhiteSpace((view as ViewResult).ViewName));
        }
    }
}
