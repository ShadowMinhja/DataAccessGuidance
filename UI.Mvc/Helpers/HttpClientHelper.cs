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
    using System.Net.Http;
    using System.Net.Http.Headers;

    public static class HttpClientHelper
    {
        public static void AddJsonRequestAcceptHeader(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}