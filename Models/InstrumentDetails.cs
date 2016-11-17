using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlowOutRentalsPrep.Models
{
    public partial class InstrumentDetails
    {
        [Key]
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public double InstrumentPrice { get; set; }
        public int RentalTypeId { get; set; }
        public string RentalType { get; set; }
        public int InstrumentPictureId { get; set; }
        public string InstrumentPicture { get; set; }
    }
}
