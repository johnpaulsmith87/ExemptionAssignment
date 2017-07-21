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
        public decimal OverdraftLimit { get; set; }
        public decimal OverdraftInterest { get; set; } //used for calc interest owed on overdraft
        public const decimal TransferFee = 1; //Not used... but was specified in assignment document
        public const decimal ATMFee = 2;
        public const decimal TellerFee = 5;
        public const decimal ChequingFee = 10;
        public BusinessCustomer Owner { get; set; }
        public BusinessAccount()
        {
            //default constructor for json
        }
        public BusinessAccount(BusinessCustomer owner, decimal initialBalance, decimal overdraftLimit)
        {
            AccountID = Guid.NewGuid();
            Owner = owner;
            OverdraftLimit = overdraftLimit;
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
