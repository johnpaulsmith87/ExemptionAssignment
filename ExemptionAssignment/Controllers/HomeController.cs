using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExemptionAssignment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            /* test code only */

           


            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
