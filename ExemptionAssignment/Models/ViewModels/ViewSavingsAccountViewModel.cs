using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class ViewSavingsAccountViewModel
    {
        public float CreditAmount { get; set; }
        public float DebitAmount { get; set; }
        public float Balance { get; set; }
        public float InterestRate { get; set; }
        public Guid AccountID { get; set; }
        public Message Message { get; set; }
    }
}
