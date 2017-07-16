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
        public List<CreditTransaction> CreditTransactionHistory { get; set; }
        public List<DebitTransaction> DebitTransactionHistory { get; set; }
        public List<AccountTransfer> TransferHistory { get; set; }
        //The following methods will return updated balances. See the child classes for implementations
        public abstract Message CalculateInterest();
        public abstract Message Debit(DebitTransaction debitDetails);
        public abstract Message Credit(CreditTransaction creditDetails);
    }
}
