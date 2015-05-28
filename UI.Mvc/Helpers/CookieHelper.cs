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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web;
    
    public static class CookieHelper
    {
        public static string GetValue(string key, string value)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            return cookie != null ? cookie[value] : null;
        }

        public static IReadOnlyDictionary<string, string> GetValues(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                var dictionary = new Dictionary<string, string>();
                foreach (string value in cookie.Values)
                {
                    dictionary.Add(value, cookie[value]);
                }

                return new ReadOnlyDictionary<string, string>(dictionary);
            }

            return null;
        }

        public static void SetValues(string key, IDictionary<string, string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var cookie = HttpContext.Current.Request.Cookies[key] ?? new HttpCookie(key);
            foreach (var value in values)
            {
                cookie[value.Key] = value.Value;
            }

            cookie.Expires = DateTime.Now.AddDays(365);
            cookie.HttpOnly = true;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}