using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlowOutRentalsPrep.Models
{
    public partial class OrderDetails
    {
        [Key]
        [Required]
        public int OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "An order date is required.")]
        public DateTime? OrderDate { get; set; }

        public int CustomerId { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustStreetAddress { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public int? CustZip { get; set; }
        public string CustEmail { get; set; }
        public string CustPhone { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public double InstrumentPrice { get; set; }
        public int RentalTypeId { get; set; }
        public string RentalType { get; set; }
        public int InstrumentPictureId { get; set; }
        public string InstrumentPicture { get; set; }
    }
}
