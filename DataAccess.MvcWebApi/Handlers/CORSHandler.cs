//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.MvcWebApi.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CorsHandler : DelegatingHandler
    {
        public const string CORSHandlerAllowerHostsSettings = "CORSHandlerAllowedHosts";

        private const string Origin = "Origin";
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        private readonly IList<string> allowedOrigins;

        public CorsHandler(string allowedOrigins)
        {
            if (string.IsNullOrWhiteSpace(allowedOrigins))
            {
                throw new ArgumentNullException("allowedOrigins");
            }

            this.allowedOrigins = new List<string>(allowedOrigins.Split(',').Select(s => s.Trim()));
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "We'll let the GC handle this.")]
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers.Contains(Origin))
            {
                if (request.Method == HttpMethod.Options)
                {
                    var requestAllowedOrigin = default(string);
                    foreach (var origin in request.Headers.GetValues(Origin))
                    {
                        if (this.allowedOrigins.Contains("*", StringComparer.Ordinal)
                            || this.allowedOrigins.Contains(new Uri(origin).Host, StringComparer.OrdinalIgnoreCase))
                        {
                            requestAllowedOrigin = origin;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(requestAllowedOrigin))
                    {
                        return Task.Factory.StartNew<HttpResponseMessage>(
                            () => new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
                    }

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Headers.Add(AccessControlAllowOrigin, requestAllowedOrigin);

                    var accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                    if (accessControlRequestMethod != null)
                    {
                        response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                    }

                    var requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                    if (!string.IsNullOrEmpty(requestedHeaders))
                    {
                        response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                    }

                    var tcs = new TaskCompletionSource<HttpResponseMessage>();
                    tcs.SetResult(response);
                    return tcs.Task;
                }
                else
                {
                    return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t =>
                    {
                        var response = t.Result;
                        response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                        return response;
                    });
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}