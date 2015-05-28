//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace UI.Mvc.Helpers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthenticatedSessionAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (AuthenticatedSessionHelper.CheckAuthenticatedSession() == AuthenticatedSessionStatus.AuthenticatedSessionExpired)
            {
                return false;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}