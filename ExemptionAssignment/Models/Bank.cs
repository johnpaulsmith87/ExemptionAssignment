using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    //Top level node for json
    //Contains all Customer and Account information
    public class Bank
    {
        public List<PrivateCustomer> PrivateCustomers { get; set; }
        public List<BusinessCustomer> BusinessCustomers { get; set; }
        public List<BusinessAccount> BusinessAccounts { get; set; }
        public List<OverdraftAccount> OverdraftAccounts { get; set; }
        public List<SavingsAccount> SavingsAccounts { get; set; }
        public List<BonusSavingsAccount> BonusSavingsAccounts { get; set; }
    }
}
