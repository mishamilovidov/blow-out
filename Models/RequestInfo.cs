using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlowOutRentalsPrep.Models
{
    public partial class RequestInfo
    {

        [Required(ErrorMessage = "Your name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your email is required."), EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A message is required.")]
        public string Message { get; set;}
    }
}
