using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlowOutRentalsPrep.Models;
using Microsoft.AspNetCore.Http;

namespace BlowOutRentalsPrep.Controllers
{
    public class ContactController : Controller
    {

        public IActionResult Index()
        {
            return View(new Contact());
        }

        // Capture Information from Contact Form and display confirmation page View Controller
        [HttpPost]
        public async Task<IActionResult> MessageSent(IFormCollection data)
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
                    if (description.Equals("Subject"))
                    {
                        var subject = data[description];
                        ViewBag.Subject = subject;
                    }
                    if (description.Equals("Message"))
                    {
                        var message = data[description];
                        ViewBag.Message = message;
                    }
                }

                return View();
            }

            return RedirectToAction("Index");
            
        }

    }
}
