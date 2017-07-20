using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExemptionAssignment.Models;
using Microsoft.AspNetCore.Hosting;
using ExemptionAssignment.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

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
            for (int i = 0; i < 3; i++)
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

        [HttpGet]
        public IActionResult SelectCustomer()
        {
            //check if there are any customers, if not return to main screen with message
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            if (bank.BusinessCustomers.Count + bank.PrivateCustomers.Count < 1)
            {
                return RedirectToAction("Index", "Home", new { message = Message.NoCustomersExist });
            }
            else
            {
                var vm = new SelectCustomerViewModel();
                //create a list with all customers send to view
                vm.Customers = new List<SelectListItem>();
                vm.Customers.AddRange(bank.PrivateCustomers.Select(pc => new SelectListItem() { Value = pc.CustomerID.ToString(), Text = pc.ContactInformation.FirstName + " " + pc.ContactInformation.LastName }));
                vm.Customers.AddRange(bank.BusinessCustomers.Select(bc => new SelectListItem() { Value = bc.CustomerID.ToString(), Text = bc.TradingName + " - " + bc.RegisteredName }));
                return View(vm);
            }
        }
        [HttpPost]
        public IActionResult SelectCustomer(IFormCollection frm)
        {
            var id = Guid.Parse(frm["CustomerID"]);
            //now use file to dig up that customer, since IDs are unique!
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            //check private first..
            var privateCustomer = bank.PrivateCustomers.SingleOrDefault(pc => pc.CustomerID == id);
            if(privateCustomer != null)
            {
                //do the things!
                return RedirectToAction("ViewAccountsById", "Account", new { id = id });
            }

            var businessCustomer = bank.BusinessCustomers.SingleOrDefault(bc => bc.CustomerID == id);
            return RedirectToAction("ViewAccountsById", "Account", new { id = id });
        }

    }
}
