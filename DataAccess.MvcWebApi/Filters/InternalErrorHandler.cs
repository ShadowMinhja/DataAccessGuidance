//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.MvcWebApi.Filters
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;
    using DataAccess.MvcWebApi.Resources;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class InternalErrorHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new ArgumentNullException("actionExecutedContext");
            }

            var message = string.Format(
                CultureInfo.CurrentCulture,
                Strings.UnexpectedError, 
                actionExecutedContext.Exception.Message);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.InternalServerError,
                message);
        }
    }
}