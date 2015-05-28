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
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Newtonsoft.Json;
using UI.Mvc.Helpers;
using UI.Mvc.Models;

    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [AuthenticatedSession]
        public ActionResult AddItem()
        {
            return this.View();
        }

        [AuthenticatedSession]
        public ActionResult Checkout()
        {
            return this.View();
        }

        [HttpPost]
        [AuthenticatedSession]
        public ActionResult Checkout(CheckoutModel checkoutModel)
        {
            if (checkoutModel == null)
            {
                throw new ArgumentNullException("checkoutModel");
            }

            var baseApiUrl = ConfigurationManager.AppSettings["baseApiUrl"];

            using (var client = new HttpClient())
            {
                HttpClientHelper.AddJsonRequestAcceptHeader(client);
                var response = client.PostAsJsonAsync(
                    baseApiUrl + "/orders",
                    new
                    {
                        CartId = checkoutModel.CartId,
                        ShippingAddressId = checkoutModel.ShippingAddress,
                        BillingAddressId = checkoutModel.BillingAddress,
                        CreditCardId = checkoutModel.CreditCard
                    }).Result;

                var status = response.StatusCode;
                
                if (status == HttpStatusCode.Created)
                {
                    // everything worked and the order was created so we return the orderId
                    Response.StatusCode = (int)status;
                    var orderId = response.Content.ReadAsAsync<string>().Result;
                    return new JsonResult() { Data = new { orderId = orderId } };
                }

                if (status == HttpStatusCode.OK)
                {
                    // the order was constructed OK, but there is an inventory/price issue that the user needs to handle
                    Response.StatusCode = (int)status;
                    var cartCheckoutItems = response.Content.ReadAsAsync<List<CartItem>>().Result;
                    return new JsonResult() { Data = new { cartCheckoutItems = cartCheckoutItems } };
                }

                var error = JsonConvert.DeserializeAnonymousType(
                        response.Content.ReadAsStringAsync().Result,
                        new { Message = string.Empty });

                return new HttpStatusCodeResult(status, error.Message);
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "orderId", Justification = "This parameter is used by the ASP.NET MVC View.")]
        public ActionResult CheckoutThanks(string orderId)
        {
            return this.View();
        }
    }
}
