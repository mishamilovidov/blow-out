using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using BlowOutRentalsPrep.Models;
using BlowOutRentalsPrep.Models.AccountViewModels;
using BlowOutRentalsPrep.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BlowOutRentalsPrep.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private BlowOutRentalsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            BlowOutRentalsContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CustomersDetail()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            IEnumerable<Customers> customers = _context.Customers.FromSql(
                "SELECT * " +
                "FROM Customers " +
                "WHERE CustActive = 1"
                );

            return View(customers);
        }

        public async Task<IActionResult> EditCustomer(int? id, bool active)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.CustomerId = id;
            ViewBag.CustActive = active;

            return View(customer);
        }

        [HttpPost, ActionName("UpdateCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer([Bind("CustomerId,CustFirstName,CustLastName,CustStreetAddress,CustCity,CustState,CustZip,CustEmail,CustPhone,CustActive")] Customers customer)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            
            if (ModelState.IsValid)
            {
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("CustomersDetail", "Admin");
            }

            return View(customer);
        }

        [HttpPost, ActionName("DeactivateCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateCustomer(int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            try
            {
                _context.Database.ExecuteSqlCommand(
                    "UPDATE Customers " +
                    "SET Customers.CustActive = 0" +
                    "WHERE Customers.CustomerID = " + id);

                return RedirectToAction("CustomersDetail", "Admin");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("OrdersDetail", new { id = id, saveChangesError = true });
            }
        }

        [HttpGet]
        public async Task<IActionResult> OrdersDetail()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            IEnumerable<OrderDetails> order_details = _context.OrderDetails.FromSql(
                "SELECT Orders.OrderNumber, Orders.OrderDate, Customers.CustomerID, Customers.CustFirstName, Customers.CustLastName, Customers.CustStreetAddress, Customers.CustCity, Customers.CustState, Customers.CustZip, Customers.CustEmail, Customers.CustPhone, Instruments.InstrumentID, Instruments.InstrumentName, Instruments.InstrumentPrice, Rental_Types.RentalTypeID, Rental_Types.RentalType, Instrument_Pictures.InstrumentPictureID, Instrument_Pictures.InstrumentPicture " +
                "FROM Orders " +
                "INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID " +
                "INNER JOIN Instruments ON Instruments.InstrumentID = Orders.InstrumentID " +
                "INNER JOIN Rental_Types ON Rental_Types.RentalTypeID = Instruments.RentalTypeID " +
                "INNER JOIN Instrument_Pictures ON Instrument_Pictures.InstrumentPictureID = Instruments.InstrumentPictureID "
                );

            return View(order_details);
        }

        [HttpPost, ActionName("CancelOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var order = await _context.Orders
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.OrderNumber == id);
            if (order == null)
            {
                return RedirectToAction("OrdersDetail", "Admin");
            }

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("OrdersDetail", "Admin");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("OrdersDetail", new { id = id, saveChangesError = true });
            }
        }

        [HttpGet]
        public async Task<IActionResult> IntsrumentsDetail()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            IEnumerable<InstrumentDetails> instrument_details = _context.InstrumentDetails.FromSql(
                "SELECT Instruments.InstrumentID, Instruments.InstrumentName, Instruments.InstrumentPrice, Rental_Types.RentalTypeID, Rental_Types.RentalType, Instrument_Pictures.InstrumentPictureID, Instrument_Pictures.InstrumentPicture " +
                "FROM Instruments " +
                "INNER JOIN Rental_Types ON Rental_Types.RentalTypeID = Instruments.RentalTypeID " +
                "INNER JOIN Instrument_Pictures ON Instrument_Pictures.InstrumentPictureID = Instruments.InstrumentPictureID;"
                );

            return View(instrument_details);
        }

        public IActionResult Error()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
