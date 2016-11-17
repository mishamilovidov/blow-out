using System;
using System.Collections.Generic;

namespace BlowOutRentalsPrep.Models
{
    public partial class RentalTypes
    {
        public RentalTypes()
        {
            Instruments = new HashSet<Instruments>();
        }

        public int RentalTypeId { get; set; }
        public string RentalType { get; set; }

        public virtual ICollection<Instruments> Instruments { get; set; }
    }
}
