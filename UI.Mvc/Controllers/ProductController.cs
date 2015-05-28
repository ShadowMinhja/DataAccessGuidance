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
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;

    public class ProductController : Controller
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "subcategoryId", Justification = "This parameter is used by the ASP.NET MVC View.")]
        public ActionResult Index(int subcategoryId)
        {
            return this.View();
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "productId", Justification = "This parameter is used by the ASP.NET MVC View.")]
        public ActionResult Detail(int productId)
        {
            return this.View();
        }
    }
}
