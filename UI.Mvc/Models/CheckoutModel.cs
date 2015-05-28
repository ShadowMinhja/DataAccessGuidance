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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CheckoutModel
    {
        public string CartId { get; set; }

        public int ShippingAddress { get; set; }

        public int BillingAddress { get; set; }

        public int CreditCard { get; set; }
    }
}