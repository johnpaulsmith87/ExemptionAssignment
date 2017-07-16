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
            InterestRate = 5.00F;
        }
        public override Message CalculateInterest()
        {
            throw new NotImplementedException();
        }

        public override Message Credit(float creditAmount)
        {
            throw new NotImplementedException();
        }

        public override Message Debit(float debitAmount)
        {
            throw new NotImplementedException();
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
            InterestRate = 4.50F;
        }
        public override Message CalculateInterest()
        {
            throw new NotImplementedException();
        }

        public override Message Credit(float creditAmount)
        {
            throw new NotImplementedException();
        }

        public override Message Debit(float debitAmount)
        {
            throw new NotImplementedException();
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

        public override Message Credit(float creditAmount)
        {
            throw new NotImplementedException();
        }

        public override Message Debit(float debitAmount)
        {
            throw new NotImplementedException();
        }
    }
}
