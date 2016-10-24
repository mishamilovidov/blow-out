using System;
using System.Collections.Generic;

namespace BlowOut.Models
{
	public partial class Instruments
	{
    	public int InstrumentId { get; set; }
    	public string InstrumentName { get; set; }
    	public int RentalTypeId { get; set; }
    	public int InstrumentPictureId { get; set; }
    	public double InstrumentPrice { get; set; }
    	public virtual InstrumentPictures InstrumentPicture { get; set; }
    	public virtual RentalTypes RentalType { get; set; }
	}
}


