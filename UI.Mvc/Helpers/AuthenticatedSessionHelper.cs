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
    using System.Text.RegularExpressions;
    using System.Web;
    using WebMatrix.WebData;

    public enum AuthenticatedSessionStatus
    {
        NotAuthenticated,
        Authenticated,
        AuthenticatedSessionExpired
    }

    public static class AuthenticatedSessionHelper
    {
        private const string SessionKeyString = "_CurrentUserSessionKey_";

        public static string CurrentUserSessionKey
        {
            get
            {
                var sessionKey = (string)HttpContext.Current.Session[SessionKeyString];

                if (string.IsNullOrWhiteSpace(sessionKey))
                {
                    sessionKey = CookieHelper.GetValue(SessionKeyString, SessionKeyString);
                    if (string.IsNullOrWhiteSpace(sessionKey))
                    {
                        sessionKey = Guid.NewGuid().ToString();
                        CookieHelper.SetValues(
                            SessionKeyString,
                            new Dictionary<string, string> { { SessionKeyString, sessionKey } });
                    }

                    HttpContext.Current.Session[SessionKeyString] = sessionKey;
                }

                return sessionKey;
            }

            set
            {
                HttpContext.Current.Session[SessionKeyString] = value;
            }
        }

        // this method checks whether session expired before auth cookie
        // **** in our case the session key should not be anonymous ****
        public static bool IsValidAuthenticatedSession
        {
            get
            {
                switch (CheckAuthenticatedSession())
                {
                    case AuthenticatedSessionStatus.Authenticated:
                        return true;

                    default:
                        return false;
                }
            }
        }

        internal static AuthenticatedSessionStatus CheckAuthenticatedSession()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return AuthenticatedSessionStatus.Authenticated;
            }
            else
            {
                return AuthenticatedSessionStatus.NotAuthenticated;
            }
        }
    }
}