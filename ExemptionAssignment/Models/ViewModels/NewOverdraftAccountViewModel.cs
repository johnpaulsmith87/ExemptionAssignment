using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class NewOverdraftAccountViewModel
    {
        public List<PrivateCustomer> CustomerList { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal OverdraftLimit { get; set; }
        public string DisplayMessage { get; set; }
    }
}
