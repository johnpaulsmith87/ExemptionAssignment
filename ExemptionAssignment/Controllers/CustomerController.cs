﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExemptionAssignment.Models;
using Microsoft.AspNetCore.Hosting;
using ExemptionAssignment.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExemptionAssignment.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHostingEnvironment _env;
        public CustomerController(IHostingEnvironment env)
        {
            _env = env; //dependency injection
        }
       public IActionResult CreateNew()
        {
            //test code only
            //Utility.Utility.TestJSON(_env.WebRootPath);

            return View();
        }
        [HttpGet]
        public IActionResult NewPrivate()
        {
            //empty form
            PrivateCustomer customer = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(), //just generate new guid at this time!
                ContactInformation = new Contact()
            };
            List<string> phoneNumbers = new List<string>();
            for(int i = 0;i < 3; i++)
            {
                phoneNumbers.Add(string.Empty);
            }
            customer.ContactInformation.PhoneNumbers = phoneNumbers;
            return View(customer);
        }
        [HttpPost]
        public IActionResult NewPrivate(PrivateCustomer customer)
        {
            //Get bank object from file, add customer to bank object, then save!
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            bank.PrivateCustomers.Add(customer);
            Utility.Utility.SaveBankData(_env.WebRootPath, bank);
            return RedirectToAction("Index", "Home", new { message = Message.CreatePrivateCustomerSuccess });
        }
        [HttpGet]
        public IActionResult NewBusiness()
        {
            //empty form
            BusinessCustomer customer = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(), //just generate new guid at this time!
                ContactInformation = new Contact()
            };
            List<string> phoneNumbers = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                phoneNumbers.Add(string.Empty);
            }
            customer.ContactInformation.PhoneNumbers = phoneNumbers;
            return View(customer);
        }
        [HttpPost]
        public IActionResult NewBusiness(BusinessCustomer customer)
        {
            //Get bank object from file, add customer to bank object, then save!
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            bank.BusinessCustomers.Add(customer);
            Utility.Utility.SaveBankData(_env.WebRootPath, bank);
            return RedirectToAction("Index", "Home", new { message = Message.CreateBusinessCustomerSuccess });
        }

    }
}
