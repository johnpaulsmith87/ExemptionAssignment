using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class ViewAccountByIdViewModel
    {
        public List<AccountSelectViewModel> Accounts { get; set; }
    }
    public class AccountSelectViewModel
    {
        public Guid AccountID { get; set; }
        public float Balance { get; set; }
        public string Type { get; set; }
        public float InterestRate { get; set; }
    }
}
