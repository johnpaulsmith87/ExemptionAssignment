using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExemptionAssignment.Models;

namespace ExemptionAssignment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(Message? message = null)
        {
            return View(message);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
