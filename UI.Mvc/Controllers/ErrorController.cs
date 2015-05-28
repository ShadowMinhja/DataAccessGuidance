//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace UI.Mvc.Controllers
{
    using System.Web.Mvc;
    
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
