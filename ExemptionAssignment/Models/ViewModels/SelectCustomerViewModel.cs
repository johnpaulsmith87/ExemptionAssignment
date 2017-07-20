using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public class SelectCustomerViewModel
    {
        public List<SelectListItem> Customers { get; set; }
    }
}
