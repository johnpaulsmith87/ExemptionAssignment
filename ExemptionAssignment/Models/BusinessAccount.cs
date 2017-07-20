using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// This class represents a business account and contains an implementation specific to the business account
    /// </summary>
    public class BusinessAccount : Account
    {
        public float OverdraftLimit { get; set; }
        public float OverdraftInterest { get; set; } //used for calc interest owed on overdraft
        public const float TransferFee = 1;
        public const float ATMFee = 2;
        public const float TellerFee = 5;
        public const float ChequingFee = 10;
        public BusinessCustomer Owner { get; set; }
        public BusinessAccount()
        {
            //default constructor for json
        }
        public BusinessAccount(BusinessCustomer owner, float initialBalance, float overdraftLimit)
        {
            AccountID = Guid.NewGuid();
            Owner = owner;
            OverdraftLimit = overdraftLimit;
            Balance = initialBalance;
            InterestRate = 3.00F;
            OverdraftInterest = 3.25F;
        }
        public override Message CalculateInterest()
        {
            if (Balance < 0)
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
            if (amount > Balance + OverdraftLimit)
            {
                return Message.ExceedsOverdraftLimit;
            }
            else if (amount > Balance)
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
