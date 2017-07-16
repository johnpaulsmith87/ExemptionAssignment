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
            //default contructor for XML
        }
        public SavingsAccount(List<PrivateCustomer> owners, float initialBalance)
        {
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.00F; //as percentage
        }
        public override Message CalculateInterest()
        {
            Balance += Balance * (InterestRate / 100);
            return Message.CalculatedInterestUpdate;
        }

        public override Message Credit(CreditTransaction creditDetails)
        {
            Balance += creditDetails.Amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(DebitTransaction debitDetails)
        {
            //need to write util class for XML fetching/saving will do before GUI work
            if (debitDetails.Amount + 1 > Balance)
                return Message.SavingsNegativeBalance;
            else
            {
                Balance -= debitDetails.Amount;
                //save XML to file or do it at controller level (probably better)
                DebitTransactionHistory.Add(debitDetails);
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
            //default constructor for XML
        }
        public BonusSavingsAccount(List<PrivateCustomer> owners, float initialBalance)
        {
            Owners = owners;
            Balance = initialBalance;
            InterestRate = 5.25F;
        }
        public override Message CalculateInterest()
        {
            
            var OrderedTransactionHistory = DebitTransactionHistory.OrderByDescending(deb => deb.Timestamp);
            if (OrderedTransactionHistory.Count() == 0 || TimeSpan.FromDays(30.0d) >= DateTime.Now.Subtract(OrderedTransactionHistory.First().Timestamp))
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

        public override Message Credit(CreditTransaction creditDetails)
        {
            Balance += creditDetails.Amount;
            return Message.AccountCreditSuccess;
        }

        public override Message Debit(DebitTransaction debitDetails)
        {
            if (debitDetails.Amount + 1 > Balance)
                return Message.SavingsNegativeBalance;
            else
            {
                Balance -= debitDetails.Amount;
                //save XML to file or do it at controller level (probably better)
                DebitTransactionHistory.Add(debitDetails);
                return Message.AccountDebitSuccess;
            }
        }
    }
    /// <summary>
    /// This class represents the private overdraft account and contains specific implementation for this type of account
    /// </summary>
    public class OverdraftAccount : PrivateAccount
    {
        public override Message CalculateInterest()
        {
            throw new NotImplementedException();
        }

        public override Message Credit(CreditTransaction creditDetails)
        {
            throw new NotImplementedException();
        }

        public override Message Debit(DebitTransaction debitDetails)
        {
            throw new NotImplementedException();
        }
    }
}
