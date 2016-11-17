using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlowOutRentalsPrep.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateData()
        {
            return RedirectToAction(nameof(Index), "Admin");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
