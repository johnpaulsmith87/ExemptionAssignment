using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    public enum Message
    {
        AccountDebitSuccess,
        AccountCreditSuccess,
        CreatePrivateCustomerSuccess,
        CreateBusinessCustomerSuccess,
        CreatePrivateAccountSuccess,
        CreateBusinessAccountSuccess,
        SavingsNegativeBalance,
        Error,
        CalculatedInterestUpdate,
        NoInterestAdded,
        ExceedsOverdraftLimit,
        OverdraftedDebit,
        NoPrivateCustomersExist,
        NoBusinessCustomersExist,
        NoCustomersExist
    }
}
