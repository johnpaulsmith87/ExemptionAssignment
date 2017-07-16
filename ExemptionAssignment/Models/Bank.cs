using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    //Top level element for XML
    //Contains all Customer and Account information
    public class Bank
    {
        public List<Customer> Customers { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
