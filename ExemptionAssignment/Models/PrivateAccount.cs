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
        public SavingsAccount(List<PrivateCustomer> owners, float initialBalance)
        {
            AccountID = Guid.NewGuid();
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.00F; //as percentage
        }
        public override Message CalculateInterest()
        {
            Balance += Balance * (InterestRate / 100);
            return Message.CalculatedInterestUpdate;
        }

        public override Message Credit(float amount)
        {
            Balance += amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(float amount)
        {
            //need to write util class for json fetching/saving will do before GUI work
            if (amount + 1 > Balance)
                return Message.SavingsNegativeBalance;
            else
            {
                Balance -= amount;
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
        public BonusSavingsAccount(List<PrivateCustomer> owners, float initialBalance)
        {
            AccountID = Guid.NewGuid();
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.25F;
            LastDebit = DateTime.Now;
        }
        public DateTime LastDebit { get; set; }
        public override Message CalculateInterest()
        {
            if (LastDebit == default(DateTime) || TimeSpan.FromDays(30) >= DateTime.Now.Subtract(LastDebit))
            {
                //add interest
                Balance += Balance * (InterestRate / 100);
                return Message.CalculatedInterestUpdate;
            }
            else
            {
                //do not add interest
                return Message.NoInterestAdded;
            }         
        }

        public override Message Credit(float amount)
        {
            Balance += amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(float amount)
        {
            if (amount + 1 > Balance)
                return Message.SavingsNegativeBalance;
            else
            {
                Balance -= amount;
                LastDebit = DateTime.Now;
                return Message.AccountDebitSuccess;
            }
        }
        public void ResetDebitCounter()
        {
            LastDebit = default(DateTime);
        }
    }
    /// <summary>
    /// This class represents the private overdraft account and contains specific implementation for this type of account
    /// </summary>
    public class OverdraftAccount : PrivateAccount
    {
        public float OverdraftLimit { get; set; }
        public float OverdraftInterest { get; set; } //used for calc interest owed on overdraft
        public OverdraftAccount()
        {
            //default constructor for json
        }
        public OverdraftAccount(float initialBalance, List<PrivateCustomer> owners, float overdraftLimit)
        {
            AccountID = Guid.NewGuid();
            OverdraftLimit = overdraftLimit;
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 3.00F;
            OverdraftInterest = 3.25F;
        }
        public override Message CalculateInterest()
        {
            if(Balance < 0)
            {
                //apply interest rate to overdraft amount
                Balance -= Balance * (OverdraftInterest / 100);
            }
            else
            {
                Balance += Balance * (InterestRate / 100);
            }
            return Message.CalculatedInterestUpdate;
        }

        public override Message Credit(float amount)
        {
            Balance += amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(float amount)
        {
            if(amount > Balance + OverdraftLimit)
            {
                return Message.ExceedsOverdraftLimit;
            }
            else if(amount > Balance)
            {
                Balance -= amount;
                return Message.OverdraftedDebit; //not sure if necessary
            }
            else
            {
                Balance -= amount;
                return Message.AccountDebitSuccess;
            }
        }
    }
}
