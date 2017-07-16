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
        public float Balance { get; set; }
        public float InterestRate { get; set; }
        public Guid AccountID { get; set; }
        public List<Transaction> TransactionHistory { get; set; }
        //The following methods will return updated balances. See the child classes for implementations
        public abstract Message CalculateInterest();
        public abstract Message Debit(float debitAmount);
        public abstract Message Credit(float creditAmount);
    }
}
