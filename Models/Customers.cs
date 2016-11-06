using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlowOut.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Your first name is required.")]
        public string CustFirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required.")]
        public string CustLastName { get; set; }

        [Required(ErrorMessage = "Your address is required.")]
        public string CustStreetAddress { get; set; }

        [Required(ErrorMessage = "Your city is required.")]
        public string CustCity { get; set; }

        [Required(ErrorMessage = "Your state is required.")]
        public string CustState { get; set; }

        [Required(ErrorMessage = "Your zip code is required.")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Entered zip code format is not valid.")]
        public int? CustZip { get; set; }

        [Required(ErrorMessage = "Your email is required."), EmailAddress]
        public string CustEmail { get; set; }

        [Required(ErrorMessage = "Your phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string CustPhone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
