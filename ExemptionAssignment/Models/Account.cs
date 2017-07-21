using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// Base class for all accounts. Contains properties shared by all accounts regardless of type.
    /// </summary>
    public abstract class Account
    {
        public decimal Balance { get; set; }
        public virtual decimal InterestRate { get; set;}
        public Guid AccountID { get; set; }

        //The following methods will return updated balances. See the child classes for implementations
        public abstract Message CalculateInterest();
        public abstract Message Debit(decimal amount);
        public abstract Message Credit(decimal amount);
    }
}
