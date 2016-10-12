using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlowOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlowOut.Controllers
{
    public class ContactController : Controller
    {

        public IActionResult Index()
        {
            var contact = new Contact()
            {
                MessageHtml = "Please call Support at <a href=\"tel:801-555-1212\">801-555-1212</a>. Thank you!"
            };

            return View(contact);
        }

        public IActionResult email(string name, string email) 
        {
            var emailsent = new Email()
            {
                MessageHtml = string.Concat("Thank you " + name + ". We will send an email to " + email + ".")
            };

            return View(emailsent);
        }

    }
}
