﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class ViewSavingsAccountViewModel
    {
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }
        public Guid AccountID { get; set; }
        public Message Message { get; set; }
    }
}
