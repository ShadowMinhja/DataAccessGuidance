//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.MvcWebApi
{
    using System;
using System.Configuration;
using System.Web.Http;
using DataAccess.MvcWebApi.Filters;
using DataAccess.MvcWebApi.Handlers;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Subcategories
            config.Routes.MapHttpRoute(
                name: "Subcategories",
                routeTemplate: "api/categories/{categoryId}/subcategories",
                defaults: new { controller = "Subcategories", action = "GetSubcategories" });
            
            // Products
            config.Routes.MapHttpRoute(
                name: "Products",
                routeTemplate: "api/subcategories/{subcategoryId}/products",
                defaults: new { controller = "Products", action = "GetProducts" });

            config.Routes.MapHttpRoute(
                name: "Recommendations",
                routeTemplate: "api/products/{productId}/recommendations",
                defaults: new { controller = "Products", action = "GetRecommendations" });

            // Orders
            config.Routes.MapHttpRoute(
                name: "OrdersHistory",
                routeTemplate: "api/account/{personId}/orders",
                defaults: new { controller = "Orders", action = "GetHistory" });

            config.Routes.MapHttpRoute(
                name: "OrderHistoryDetails",
                routeTemplate: "api/orders/history/{historyId}",
                defaults: new { controller = "Orders", action = "GetHistoryDetails" });

            // Default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional },
                constraints: new { action = "[a-zA-Z]+" });

            config.Routes.MapHttpRoute(
                name: "DefaultApiFallback",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // add message handler to handle CORS preflight requests            
            config.MessageHandlers.Add(new CorsHandler(ConfigurationManager.AppSettings[CorsHandler.CORSHandlerAllowerHostsSettings]));
            
            // add global filter to catch all unhandled errors
            config.Filters.Add(new InternalErrorHandlerAttribute());

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            // config.EnableQuerySupport();
        }
    }
}
