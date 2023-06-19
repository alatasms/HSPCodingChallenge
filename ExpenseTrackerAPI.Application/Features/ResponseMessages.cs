using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features
{
    public enum ResponseMessages
    {
        UserCreated,
        UserLoggedInSuccessfully,
        UserLoggedInFailed,
        WrongEmailOrPassword,
        AccounCreatedSuccesfully,
        InsufficientBalance,
        AccountUpdated,
        InvalidBalance,
        AccountDeleted,
        TransactionDeleted,
        AccessDenied,
        TransactionUpdated
    }
}
