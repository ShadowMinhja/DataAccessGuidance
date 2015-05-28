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
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Login email")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "Email Address cannot be longer than 50 characters")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and not exceed 50 characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Address cannot be longer than 50 characters")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(30, ErrorMessage = "City cannot be longer than 30 characters")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string StateCode { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(10, ErrorMessage = "Postal Code cannot be longer than 10 characters")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        [StringLength(20)]
        public string CardType { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [RegularExpression("^[0-9]{13,16}$", ErrorMessage = "Card Number should contain 13-16 digits")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Month")]
        [Range(01, 12, ErrorMessage = "Expiration month should be between 01 and 12")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Incorrect expiration month format")]
        public int ExpMonth { get; set; }

        [Required]
        [Display(Name = "Expiration Year")]
        [Range(2013, 2099, ErrorMessage = "Expiration year should be between 2013 and 2099")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Incorrect expiration year format")]
        public int ExpYear { get; set; }
    }
}