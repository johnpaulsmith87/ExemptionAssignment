﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class NewBusinessAccountViewModel
    {
        public List<BusinessCustomer> CustomerList { get; set; }
        public float InitialBalance { get; set; }
        public float OverdraftLimit { get; set; }
        public string DisplayMessage { get; set; }
    }
}
