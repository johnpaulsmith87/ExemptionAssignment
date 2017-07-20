using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ExemptionAssignment.Utility;
using ExemptionAssignment.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExemptionAssignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHostingEnvironment _env;
        public AccountController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PrivateType()
        {
            //check if any private accounts exist! if not send them back to the main page with message
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            if (bank.PrivateCustomers.Count < 1)
            {
                return RedirectToAction("Index", "Home", new { message = Message.NoPrivateCustomersExist });
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult NewBusiness()
        {
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            if (bank.BusinessCustomers.Count < 1)
            {
                return RedirectToAction("Index", "Home", new { message = Message.NoBusinessCustomersExist });
            }
            else
            {
                //create new entity and send it in as viewmodel for view
                return null;
            }
        }
        [HttpGet]
        public IActionResult CreateNewSavings()
        {
            NewSavingsAccountViewModel vm = new NewSavingsAccountViewModel();
            vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).PrivateCustomers;
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateNewSavings(NewSavingsAccountViewModel vm)
        {
            if (!vm.CustomerList.Any(pc => pc.IsSelected))
            {
                vm.DisplayMessage = "Must select at least one account owner";
                return View(vm);
            }
            else if (vm.InitialBalance < 0)
            {
                vm.DisplayMessage = "Please input a positive sum for initial balance";
                return View(vm);
            }
            else
            {
                //get bank data, then save account to file and send back to start page
                var bank = Utility.Utility.GetBankData(_env.WebRootPath);
                var selectedOwners = vm.CustomerList.Where(pc => pc.IsSelected); // small issue, but I prefer to use my file as the proper datastore!
                var owners = new List<PrivateCustomer>();
                foreach(var owner in selectedOwners)
                {
                    owners.Add(bank.PrivateCustomers.Single(pc => pc.CustomerID == owner.CustomerID));
                }
                var account = new SavingsAccount(owners, vm.InitialBalance);
                bank.SavingsAccounts.Add(account);
                //now save to file
                Utility.Utility.SaveBankData(_env.WebRootPath, bank);
                return RedirectToAction("Index", "Home", new { message = Message.CreatePrivateAccountSuccess });
            }
        }
        [HttpGet]
        public IActionResult CreateNewBonus()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult CreateNewBonus(BonusSavingsAccount account)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IActionResult CreateNewOverdraft()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult CreateNewOverdraft(SavingsAccount account)
        {
            throw new NotImplementedException();
        }

    }
}
