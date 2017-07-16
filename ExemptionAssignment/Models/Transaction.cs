using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public abstract class Transaction
    {
        public float Amount { get; set; }
        public DateTime Timestamp { get; set; }

    }
    public class CreditTransaction : Transaction
    {
        public CreditTransaction()
        {
            //default constructor for XML
        }    
        public CreditTransaction(Account accountCredited, float amount)
        {
            AccountCredited = accountCredited;
            Amount = amount;
            Timestamp = DateTime.Now;
        }
        public Account AccountCredited { get; set; }
    }
    public class DebitTransaction : Transaction
    {
        public DebitTransaction()
        {
            //default constructor for XML
        }
        public DebitTransaction(Account accountDebited, float amount)
        {
            AccountDebited = accountDebited;
            Amount = amount;
            Timestamp = DateTime.Now;
        }
        public Account AccountDebited { get; set; }
        
    }
    public class AccountTransfer : Transaction
    {
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public AccountTransfer()
        {
            //default contructor for Transfers
        }
        public AccountTransfer(Account toAccount, Account fromAccount, float amount)
        {
            Timestamp = DateTime.Now;
            FromAccount = fromAccount;
            ToAccount = ToAccount;
        }
    }
}
