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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using UI.Mvc.Helpers;

    public class OrderController : Controller
    {
        [AuthenticatedSession]
        public ActionResult Index()
        {
            return this.View();
        }

        [AuthenticatedSession]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "historyId", Justification = "This parameter is used by the ASP.NET MVC View.")]
        public ActionResult Detail(string historyId)
        {
            return this.View();
        }
    }
}