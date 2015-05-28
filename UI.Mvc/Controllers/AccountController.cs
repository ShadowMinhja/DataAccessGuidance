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
    using System.Globalization;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using UI.Mvc.Helpers;
    using UI.Mvc.Models;
    using WebMatrix.WebData;
    
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(Uri returnUrl)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Category");
            }

            ViewBag.ReturnUrl = returnUrl != null ? returnUrl.OriginalString : null;
            return this.View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Authenticate(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Already authenticated, please logoff first..");
            }

            try
            {
                if (WebSecurity.Login(account.UserName, account.Password))
                {
                    return new JsonResult() { Data = new { userId = AuthenticatedSessionHelper.CurrentUserSessionKey } };
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "User not authenticated");
                }
            }
            catch (HttpException ex)
            {
                return new HttpStatusCodeResult(ex.GetHttpCode(), ex.Message);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            AuthenticatedSessionHelper.CurrentUserSessionKey = string.Empty;

            return this.RedirectToAction("Index", "Category");
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Category");
            }

            return this.View(new RegisterModel { });
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            // Response.StatusCode = (int)HttpStatusCode.OK; 
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Json(new { registerSuccess = false, error = "Already authenticated, please logoff first.." });
            }

            if (ModelState.IsValid && (model != null))
            {
                try
                {
                    var registrationInfo = new RegistrationInfo
                    {
                        EmailAddresses = new List<string> { model.EmailAddress },
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        Addresses = new List<AddressWithStateCode> 
                        { 
                            new AddressWithStateCode 
                            {
                                AddressLine1 = model.Address,
                                City = model.City,
                                PostalCode = model.PostalCode,
                                StateProvinceId = Convert.ToInt32(model.StateCode, CultureInfo.InvariantCulture)
                            }
                        },
                        CreditCards = new List<CreditCardWithExpiration>
                        {
                            new CreditCardWithExpiration
                            {
                                CardNumber = model.CardNumber,
                                CardType = model.CardType,
                                ExpMonth = model.ExpMonth,
                                ExpYear = model.ExpYear
                            }
                        }
                    };

                    // Attempt to register the user
                    WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, new { RegistrationInfo = registrationInfo });
                    if (WebSecurity.Login(model.EmailAddress, model.Password))
                    {
                        return this.Json(new { registerSuccess = true });
                    }

                    ModelState.AddModelError("Registration Error", ErrorCodeToString(MembershipCreateStatus.ProviderError));
                }
                catch (MembershipCreateUserException ex)
                {
                    return this.Json(new { registerSuccess = false, error = ex.Message });
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Json(new { registerSuccess = false, error = "Unknown Error" });
        }

        [AllowAnonymous]
        public JsonResult GetCurrentUserSessionKey()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            return this.Json(new { userSessionKey = AuthenticatedSessionHelper.CurrentUserSessionKey }, JsonRequestBehavior.AllowGet);
        }
        
        #region Helpers
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}