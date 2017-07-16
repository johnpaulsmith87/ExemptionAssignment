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
        public BusinessAccount()
        {
            //Default constructor for XML serialisation/deserialisation
        }
        public BusinessCustomer Owner { get; set; }
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
