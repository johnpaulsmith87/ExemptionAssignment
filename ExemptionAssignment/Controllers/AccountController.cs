using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ExemptionAssignment.Utility;
using ExemptionAssignment.Models;
using Microsoft.AspNetCore.Http;

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
                NewBusinessAccountViewModel vm = new NewBusinessAccountViewModel();
                vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).BusinessCustomers;
                return View(vm);
            }
        }
        [HttpPost]
        public IActionResult NewBusiness(NewBusinessAccountViewModel vm, IFormCollection frm)
        {



            if (string.IsNullOrEmpty(frm["CustomerID"]))
            {
                vm.DisplayMessage = "Must select at least one account owner";
                //unfortunately we have to rebind customers to customer list here
                vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).BusinessCustomers;
                return View(vm);
            }
            else if (vm.InitialBalance < 0)
            {
                vm.DisplayMessage = "Please input a positive sum for initial balance";
                vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).BusinessCustomers;
                return View(vm);
            }
            else if (vm.OverdraftLimit < 0)
            {
                vm.DisplayMessage = "Please input a positive sum for overdraft limit";
                vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).BusinessCustomers;
                return View(vm);
            }
            else
            {
                //get bank data, then save account to file and send back to start page
                var id = Guid.Parse(frm["CustomerID"]);
                var bank = Utility.Utility.GetBankData(_env.WebRootPath);
                var selectedOwner = bank.BusinessCustomers.Single(bc => id == bc.CustomerID);
                var account = new BusinessAccount(selectedOwner, vm.InitialBalance, vm.OverdraftLimit);
                bank.BusinessAccounts.Add(account);
                //now save to file
                Utility.Utility.SaveBankData(_env.WebRootPath, bank);
                return RedirectToAction("Index", "Home", new { message = Message.CreateBusinessAccountSuccess });
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
                foreach (var owner in selectedOwners)
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
            NewBonusSavingsAccountViewModel vm = new NewBonusSavingsAccountViewModel();
            vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).PrivateCustomers;
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateNewBonus(NewBonusSavingsAccountViewModel vm)
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
                var selectedOwners = vm.CustomerList.Where(pc => pc.IsSelected);
                var owners = new List<PrivateCustomer>();
                foreach (var owner in selectedOwners)
                {
                    owners.Add(bank.PrivateCustomers.Single(pc => pc.CustomerID == owner.CustomerID));
                }
                var account = new BonusSavingsAccount(owners, vm.InitialBalance);
                bank.BonusSavingsAccounts.Add(account);
                //now save to file
                Utility.Utility.SaveBankData(_env.WebRootPath, bank);
                return RedirectToAction("Index", "Home", new { message = Message.CreatePrivateAccountSuccess });
            }
        }
        [HttpGet]
        public IActionResult CreateNewOverdraft()
        {
            NewOverdraftAccountViewModel vm = new NewOverdraftAccountViewModel();
            vm.CustomerList = Utility.Utility.GetBankData(_env.WebRootPath).PrivateCustomers;
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateNewOverdraft(NewOverdraftAccountViewModel vm)
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
            else if (vm.OverdraftLimit < 0)
            {
                vm.DisplayMessage = "Please input a positive sum for overdraft limit";
                return View(vm);
            }
            else
            {
                //get bank data, then save account to file and send back to start page
                var bank = Utility.Utility.GetBankData(_env.WebRootPath);
                var selectedOwners = vm.CustomerList.Where(pc => pc.IsSelected);
                var owners = new List<PrivateCustomer>();
                foreach (var owner in selectedOwners)
                {
                    owners.Add(bank.PrivateCustomers.Single(pc => pc.CustomerID == owner.CustomerID));
                }
                var account = new OverdraftAccount(vm.InitialBalance, owners, vm.OverdraftLimit);
                bank.OverdraftAccounts.Add(account);
                //now save to file
                Utility.Utility.SaveBankData(_env.WebRootPath, bank);
                return RedirectToAction("Index", "Home", new { message = Message.CreatePrivateAccountSuccess });
            }

        }
        [HttpGet]
        public IActionResult ViewAccountsById(Guid id)
        {
            //first find the customer by id, then find all accounts and then send them to the view
            var bank = Utility.Utility.GetBankData(_env.WebRootPath);
            var privateCustomer = bank.PrivateCustomers.SingleOrDefault(pc => pc.CustomerID == id);
            ViewAccountByIdViewModel vm = new ViewAccountByIdViewModel();
            vm.Accounts = new List<AccountSelectViewModel>();
            if (privateCustomer == null)
            {
                var accounts = bank.BusinessAccounts.Where(ba => ba.Owner.CustomerID == id).ToList();
                vm.Accounts.AddRange(accounts.Select(acc => new AccountSelectViewModel()
                {
                    AccountID = acc.AccountID,
                    Balance = acc.Balance,
                    InterestRate = acc.InterestRate,
                    Type = "Business"
                }));
            }
            else
            {
                //since it is private we must do as we did above but for each type :( I wish I had set up my file better
                //Savings
                List<SavingsAccount> savingsAccounts = new List<SavingsAccount>();
                foreach (var acc in bank.SavingsAccounts)
                    foreach (var owner in acc.Owners)
                        if (owner.CustomerID == id)
                            savingsAccounts.Add(acc);
                vm.Accounts.AddRange(savingsAccounts.Select(acc => new AccountSelectViewModel()
                {
                    AccountID = acc.AccountID,
                    Balance = acc.Balance,
                    InterestRate = acc.InterestRate,
                    Type = "Savings"
                }));
                //bonus savings
                List<BonusSavingsAccount> bonusAccounts = new List<BonusSavingsAccount>();
                foreach (var acc in bank.BonusSavingsAccounts)
                    foreach (var owner in acc.Owners)
                        if (owner.CustomerID == id)
                            bonusAccounts.Add(acc);

                vm.Accounts.AddRange(bonusAccounts.Select(acc => new AccountSelectViewModel()
                {
                    AccountID = acc.AccountID,
                    Balance = acc.Balance,
                    InterestRate = acc.InterestRate,
                    Type = "Bonus Savings"
                }));
                //overdraft
                List<OverdraftAccount> overdraftAccounts = new List<OverdraftAccount>();
                foreach (var acc in bank.OverdraftAccounts)
                    foreach (var owner in acc.Owners)
                        if (owner.CustomerID == id)
                            overdraftAccounts.Add(acc);

                vm.Accounts.AddRange(overdraftAccounts.Select(acc => new AccountSelectViewModel()
                {
                    AccountID = acc.AccountID,
                    Balance = acc.Balance,
                    InterestRate = acc.InterestRate,
                    Type = "Overdraft"
                }));
            }

            return View(vm);
        }
    }
}
