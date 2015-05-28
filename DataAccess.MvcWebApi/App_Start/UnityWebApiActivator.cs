//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DataAccess.MvcWebApi.App_Start.UnityWebApiActivator), "Start")]

namespace DataAccess.MvcWebApi.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
            var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}
