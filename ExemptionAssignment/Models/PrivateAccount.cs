using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// Represents the base class for all private accounts and as such contains all properties shared by private accounts
    /// i.e Customers, Balance, Interest Rate etc...
    /// </summary>
    public abstract class PrivateAccount : Account
    {
        //using List because it can be 1 or more.
        public List<PrivateCustomer> Owners { get; set; }
    }
    /// <summary>
    /// This class represents the private savings account and contains specific implementation for this type of account
    /// </summary>
    public class SavingsAccount : PrivateAccount
    {
        public SavingsAccount()
        {
            //default contructor for json
        }
        public SavingsAccount(List<PrivateCustomer> owners, decimal initialBalance)
        {
            AccountID = Guid.NewGuid();
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.00M; //as percentage
        }
        public override Message CalculateInterest()
        {
            Balance += Balance * (InterestRate / 100);
            Balance = Math.Round(Balance, 2);
            return Message.CalculatedInterestUpdate;
        }

        public override Message Credit(decimal amount)
        {
            if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            else
            {
                Balance += amount;
                return Message.AccountCreditSuccess;
            }
        }

        public override Message Debit(decimal amount)
        {
            //need to write util class for json fetching/saving will do before GUI work
            if (amount + 1 > Balance)
                return Message.SavingsNegativeBalance;
            else if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            else
            {
                Balance -= amount + 1;
                //save json to file or do it at controller level (probably better)
                return Message.AccountDebitSuccess;
            }
        }
    }
    /// <summary>
    /// This class represents the private bonus savings account and contains specific implementation for this type of account
    /// </summary>
    public class BonusSavingsAccount : PrivateAccount
    {

        public BonusSavingsAccount()
        {
            //default constructor for json
        }
        public BonusSavingsAccount(List<PrivateCustomer> owners, decimal initialBalance)
        {
            AccountID = Guid.NewGuid();
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.25M;
            LastDebit = DateTime.Now;
        }
        public DateTime LastDebit { get; set; }
        public override Message CalculateInterest()
        {
            if (LastDebit == default(DateTime) || (DateTime.Now - LastDebit).TotalDays >= 30)
            {
                //add interest
                Balance += Balance * (InterestRate / 100);
                Balance = Math.Round(Balance, 2);
                return Message.CalculatedInterestUpdate;
            }
            else
            {
                //do not add interest
                return Message.NoInterestAdded;
            }
        }

        public override Message Credit(decimal amount)
        {
            if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            Balance += amount;

            return Message.AccountCreditSuccess;
        }

        public override Message Debit(decimal amount)
        {
            if (amount > Balance)
                return Message.SavingsNegativeBalance;
            else if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            else
            {
                Balance -= amount;
                LastDebit = DateTime.Now;
                return Message.AccountDebitSuccess;
            }
        }
        public Message ResetDebitCounter()
        {
            LastDebit = default(DateTime);
            return Message.ResetDebitCounter;
        }
    }
    /// <summary>
    /// This class represents the private overdraft account and contains specific implementation for this type of account
    /// </summary>
    public class OverdraftAccount : PrivateAccount
    {
        public decimal OverdraftLimit { get; set; }
        public decimal OverdraftInterest { get; set; } //used for calc interest owed on overdraft
        public OverdraftAccount()
        {
            //default constructor for json
        }
        public OverdraftAccount(decimal initialBalance, List<PrivateCustomer> owners, decimal overdraftLimit)
        {
            AccountID = Guid.NewGuid();
            OverdraftLimit = overdraftLimit;
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 3.00M;
            OverdraftInterest = 3.25M;
        }
        public override Message CalculateInterest()
        {
            if (Balance < 0)
            {
                //apply interest rate to overdraft amount
                var interestOwed = -Balance * (OverdraftInterest / 100);
                Balance -= interestOwed;
            }
            else
            {

                Balance += Balance * (InterestRate / 100);           
            }
            Balance = Math.Round(Balance, 2);
            return Message.CalculatedInterestUpdate;
        }

        public override Message Credit(decimal amount)
        {
            if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            Balance += amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(decimal amount)
        {
            if (amount > Balance + OverdraftLimit)
            {
                return Message.ExceedsOverdraftLimit;
            }
            else if (!(amount > 0))
            {
                return Message.AmountMustBeGreaterThanZero;
            }
            else if (amount > Balance)
            {
                Balance -= amount;
                return Message.OverdraftedDebit; 
            }
            else
            {
                Balance -= amount;
                return Message.AccountDebitSuccess;
            }
        }
    }
}
