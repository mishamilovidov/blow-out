using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlowOut.Models;
using Microsoft.AspNetCore.Http;

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

        public IActionResult RequestInformation(string instname, float price, string picture, string type)
        {
            ViewBag.InstrumentName = instname;
            ViewBag.InstrumentPrice = price;
            ViewBag.InstrumentPicture = picture;
            ViewBag.RentalType = type;

            return View(new RequestInfo());
        }

        [HttpPost]
        public async Task<IActionResult> MessageSent(IFormCollection data, string instname, string type)
        {
            if (ModelState.IsValid) 
            {
                foreach (string description in data.Keys)
                {
                    if (description.Equals("Name"))
                    {
                        var name = data[description];
                        ViewBag.Name = name;
                    }
                    if (description.Equals("Email"))
                    {
                        var email = data[description];
                        ViewBag.Email = email;
                    }
                    if (description.Equals("Message"))
                    {
                        var message = data[description];
                        ViewBag.Message = message;
                    }
                }

                ViewBag.InstrumentName = instname;
                ViewBag.RentalType = type;

                return View();
            }

            return RedirectToAction("Index");
            
        }
    }
}
