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
        public override float CalculateInterest()
        {
            throw new NotImplementedException();
        }
        public override float Credit(float creditAmount)
        {
            throw new NotImplementedException();
        }
        public override float Debit(float debitAmount)
        {
            throw new NotImplementedException();
        }
    }
}
