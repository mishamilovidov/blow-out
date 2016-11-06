using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BlowOut.Models
{
	public class RentalInfo
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