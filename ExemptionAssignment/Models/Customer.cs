using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// Base class for all customers. Abstract only used for inheritence. Contains properties that apply to all customers
    /// whether business or private.
    /// </summary>
    public abstract class Customer
    {
        //Guid is used here to make creating new customers simpler (i.e no need to check existing ids then iterate)
        public Guid CustomerID { get; set; }
        public Contact ContactInformation { get; set; }

    }
}
