using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public abstract class Transaction
    {
        public float Amount { get; set; }
    }
    public class CreditTransaction : Transaction
    {

    }
    public class DebitTransaction : Transaction
    {

    }
    public class AccountTransfer : Transaction
    {

    }
}
