//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace UI.Mvc
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Categories",
                url: "categories",
                defaults: new { controller = "Category", action = "Index" });

            routes.MapRoute(
                name: "SubCategories",
                url: "categories/{categoryId}/subcategories",
                defaults: new { controller = "SubCategory", action = "Index" });

            routes.MapRoute(
                name: "Products",
                url: "subcategories/{subcategoryId}/products",
                defaults: new { controller = "Product", action = "Index" });

            routes.MapRoute(
                name: "ProductDetail",
                url: "products/{productId}",
                defaults: new { controller = "Product", action = "Detail" });

            routes.MapRoute(
                name: "Orders",
                url: "orders",
                defaults: new { controller = "Order", action = "Index" });

            routes.MapRoute(
                name: "OrderDetail",
                url: "orders/{historyId}",
                defaults: new { controller = "Order", action = "Detail" });

            routes.MapRoute(
                name: "CheckoutThank",
                url: "cart/checkoutthanks/{orderId}",
                defaults: new { controller = "Cart", action = "CheckoutThanks" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional });
        }
    }
}