using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ExemptionAssignment.Models;
using System.IO;

namespace ExemptionAssignment.Utility
{
    /// <summary>
    /// Static class for dealing with Saving/Loading json to/from file.
    /// </summary>
    public static class Utility
    {
        public static Bank GetBankData(string path)
        {
            string localPath = Path.Combine(path, JSONPath);
            if (!File.Exists(localPath))
            {
                return new Bank() {
                    PrivateCustomers = new List<PrivateCustomer>(),
                    BusinessCustomers = new List<BusinessCustomer>(),
                    BonusSavingsAccounts = new List<BonusSavingsAccount>(),
                    BusinessAccounts = new List<BusinessAccount>(),
                    OverdraftAccounts = new List<OverdraftAccount>(),
                    SavingsAccounts  = new List<SavingsAccount>()
                }; //if a file doesn't exist just return a new bank with empty lists (otherwise lists will be null, saves having to check in
                //other places
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
            var jsonString = JsonConvert.SerializeObject(bank, Formatting.Indented);
            File.WriteAllText(localPath, jsonString);
        }
        private static string JSONPath = "../bank.json";

        /* this is code for testing json serialisation/deserialisation!
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
                RegisteredName = "Acme Inc",
                TradingName = "ACE"
            };
            SavingsAccount sa = new SavingsAccount(new List<PrivateCustomer>() { pc }, 100.25f);
            BusinessAccount ba = new BusinessAccount(bc, 11236.12f, 10000);

            Bank bank = new Bank()
            {
                SavingsAccounts = new List<SavingsAccount> { sa },
                PrivateCustomers = new List<PrivateCustomer> { pc },
                BusinessCustomers = new List<BusinessCustomer> { bc },
                BusinessAccounts = new List<BusinessAccount> { ba }
            };
            SaveBankData(path, bank);

            var loadTest = GetBankData(path);

        }
        */
    }
}
