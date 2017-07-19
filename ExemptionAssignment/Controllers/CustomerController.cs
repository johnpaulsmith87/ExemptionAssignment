using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExemptionAssignment.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExemptionAssignment.Controllers
{
    public class CustomerController : Controller
    {
       public IActionResult CreateNew()
        {
            return View();
        }
        public IActionResult NewPrivate()
        {
            PrivateCustomer customer = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(), //just generate new guid at this time!
                ContactInformation = new Contact()
            };
            return View(customer);
        }
        
    }
}
