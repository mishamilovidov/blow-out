using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlowOut.Models;

namespace BlowOut.Controllers
{
    public class RentalsController : Controller
    {
        private BlowOutContext _context;

    	public RentalsController(BlowOutContext context)
    	{
        	_context = context;
    	}

    	// GET: /<controller>/
    	public IActionResult Index()
    	{
            IEnumerable<RentalInfo> instruments = _context.RentalInfo.FromSql(
            	"SELECT Instruments.InstrumentId, Instruments.InstrumentName, Instruments.InstrumentPrice, Rental_Types.RentalTypeID, Rental_Types.RentalType, Instrument_Pictures.InstrumentPictureID, Instrument_Pictures.InstrumentPicture FROM Instruments INNER JOIN Rental_Types ON Rental_Types.RentalTypeID = Instruments.RentalTypeID INNER JOIN Instrument_Pictures ON Instrument_Pictures.InstrumentPictureID = Instruments.InstrumentPictureID"
            	);

        	return View(instruments);
    	}
    }
}
