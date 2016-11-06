using System;
using System.Collections.Generic;

namespace BlowOut.Models
{
    public partial class Orders
    {
        public int OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? InstrumentId { get; set; }
        public int? CustomerId { get; set; }
        public bool? Rental { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Instruments Instrument { get; set; }
    }
}
