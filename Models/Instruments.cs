using System;
using System.Collections.Generic;

namespace BlowOutRentalsPrep.Models
{
    public partial class Instruments
    {
        public Instruments()
        {
            Orders = new HashSet<Orders>();
        }

        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public int RentalTypeId { get; set; }
        public int InstrumentPictureId { get; set; }
        public double InstrumentPrice { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual InstrumentPictures InstrumentPicture { get; set; }
        public virtual RentalTypes RentalType { get; set; }
    }
}
