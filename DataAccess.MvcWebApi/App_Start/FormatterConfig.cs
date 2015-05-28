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
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp;

    public static class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            if (formatters == null)
            {
                throw new ArgumentNullException("formatters");
            }

            formatters.Remove(formatters.JsonFormatter);

            formatters.Insert(
                0,                 
                new JsonpMediaTypeFormatter
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                });
        }
    }
}