using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ExemptionAssignment.Models;
using System.IO;

namespace ExemptionAssignment.Utility
{
    public static class Utility
    {
        public static Bank GetBankData(string path)
        {
            string localPath = Path.Combine(path, JSONPath);
            if (!File.Exists(localPath))
            {
                return default(Bank); //if a file doesn't exist, just return a null
            }
            else
            {
                var jsonString = File.ReadAllText(localPath);
                return JsonConvert.DeserializeObject<Bank>(jsonString);
            }
        }
        public static void SaveBankData(string path, Bank bank)
        {
            string localPath = Path.Combine(path, JSONPath);
            var jsonString = JsonConvert.SerializeObject(bank);
            File.WriteAllText(localPath, jsonString);
        }
        private static string JSONPath = "../bank.json";

        public static void TestJSON(string path)
        {
            //create some test objects
            PrivateCustomer pc = new PrivateCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "jp",
                    LastName = "smith",
                    Address = "jjjj",
                    Email = "jjj",
                    PhoneNumbers = new List<string>() { "123-122" }
                }
            };
            BusinessCustomer bc = new BusinessCustomer()
            {
                CustomerID = Guid.NewGuid(),
                ContactInformation = new Contact()
                {
                    FirstName = "jps",
                    LastName = "smiths",
                    Address = "sss",
                    Email = "jjj@hsh.com",
                    PhoneNumbers = new List<string>() { "123-122" }
                },
                RegisteredName = "Evil Corp",
                TradingName = "EVC"
            };
            SavingsAccount sa = new SavingsAccount(new List<PrivateCustomer>() { pc }, 100.25f);
            BusinessAccount ba = new BusinessAccount(bc, 11236.12f, 10000);

            Bank bank = new Bank();
            bank.SavingsAccounts = new List<SavingsAccount> { sa };
            bank.PrivateCustomers = new List<PrivateCustomer> { pc };
            bank.BusinessCustomers = new List<BusinessCustomer> { bc };
            bank.BusinessAccounts = new List<BusinessAccount> { ba };

            SaveBankData(path, bank);

            var loadTest = GetBankData(path);

        }
    }
}
