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
    public class CreditCardWithExpiration : CreditCard
    {
        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }
    }
}