//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace UI.Mvc.Models
{
    using System.Collections.Generic;

    public class RegistrationInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> EmailAddresses { get; set; }

        public string Password { get; set; }

        public IEnumerable<AddressWithStateCode> Addresses { get; set; }

        public IEnumerable<CreditCardWithExpiration> CreditCards { get; set; }
    }
}