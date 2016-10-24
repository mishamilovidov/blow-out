using System;
using System.Collections.Generic;
namespace BlowOut.Models
{
	public partial class InstrumentPictures
	{
    	public InstrumentPictures()
    	{
        	Instruments = new HashSet<Instruments>();
    	}
    	public int InstrumentPictureId { get; set; }
    	public string InstrumentPicture { get; set; }
    	public virtual ICollection<Instruments> Instruments { get; set; }
	}
}

