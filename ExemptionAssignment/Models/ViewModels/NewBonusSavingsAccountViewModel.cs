using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class NewBonusSavingsAccountViewModel
    {
        public List<PrivateCustomer> CustomerList { get; set; }
        public float InitialBalance { get; set; }

        public string DisplayMessage { get; set; }
    }
}
