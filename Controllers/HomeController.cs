using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlowOut.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult about()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
