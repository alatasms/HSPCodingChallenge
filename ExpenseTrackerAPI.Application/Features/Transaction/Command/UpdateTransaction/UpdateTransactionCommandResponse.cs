using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Command.UpdateTransaction
{
    public class UpdateTransactionCommandResponse: BaseResponse
    {
        public object? Transaction { get; set; }

    }
}
