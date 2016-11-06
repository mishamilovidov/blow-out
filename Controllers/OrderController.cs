using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlowOut.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BlowOut.Controllers
{
    public class OrderController : Controller
    {
        private BlowOutContext _context;

    	public OrderController(BlowOutContext context)
    	{
        	_context = context;
    	}

        public IActionResult Index(int id, string instname, float price, string picture, string type)
        {
            ViewBag.InstrumentId = id;
            ViewBag.InstrumentName = instname;
            ViewBag.InstrumentPrice = price;
            ViewBag.InstrumentPicture = picture;
            ViewBag.RentalType = type;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder([Bind("CustFirstName,CustLastName,CustStreetAddress,CustCity,CustState,CustZip,CustEmail,CustPhone")] Customers customer, IFormCollection data, int id, string instname, float price, string picture, string type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    foreach (string description in data.Keys)
                    {
                        if (description.Equals("CustFirstName"))
                        {
                            var fname = data[description];
                            ViewBag.CustFirstName = fname;
                        }
                        if (description.Equals("CustLastName"))
                        {
                            var lname = data[description];
                            ViewBag.CustLastName = lname;
                        }
                        if (description.Equals("CustStreetAddress"))
                        {
                            var address = data[description];
                            ViewBag.CustStreetAddress = address;
                        }
                        if (description.Equals("CustCity"))
                        {
                            var city = data[description];
                            ViewBag.CustCity = city;
                        }
                        if (description.Equals("CustState"))
                        {
                            var state = data[description];
                            ViewBag.CustState = state;
                        }
                        if (description.Equals("CustZip"))
                        {
                            var zip = data[description];
                            ViewBag.CustZip = zip;
                        }
                        if (description.Equals("CustEmail"))
                        {
                            var email = data[description];
                            ViewBag.CustEmail = email;
                        }
                        if (description.Equals("CustPhone"))
                        {
                            var phone = data[description];
                            ViewBag.CustPhone = phone;
                        }
                    }

                    int custid = _context.Customers
                       .OrderByDescending(q => q.CustomerId)
                       .Select(q => q.CustomerId)
                       .FirstOrDefault();

                    ViewBag.CustomerId = custid;
                    ViewBag.InstrumentId = id;
                    ViewBag.InstrumentName = instname;
                    ViewBag.InstrumentPrice = price;
                    ViewBag.InstrumentPicture = picture;
                    ViewBag.RentalType = type;

                    return View();
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder([Bind("OrderDate,InstrumentId,CustomerId")] Orders order, int id, string instname, float price, string picture, string type, string name, string address, string email, string phone, float fullprice, DateTime date)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(order);
                    await _context.SaveChangesAsync();

                    int ordernumber = _context.Orders
                       .OrderByDescending(q => q.OrderNumber)
                       .Select(q => q.OrderNumber)
                       .FirstOrDefault();

                    ViewBag.OrderNumber = ordernumber;
                    ViewBag.InstrumentId = id;
                    ViewBag.InstrumentName = instname;
                    ViewBag.InstrumentPrice = price;
                    ViewBag.InstrumentPicture = picture;
                    ViewBag.RentalType = type;
                    ViewBag.CustFullName = name;
                    ViewBag.CustAddress = address;
                    ViewBag.CustEmail = email;
                    ViewBag.CustPhone = phone;
                    ViewBag.InstrumentFullPrice = fullprice;
                    ViewBag.OrderDate = date;

                    return View();
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(order);

        }
    }
}
