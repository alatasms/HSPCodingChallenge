namespace ExpenseTrackerAPI.Application.Features.Transaction.Command.CreateTransaction
{
    public class CreateTransactionCommandResponse : BaseResponse
    {
        public object? Transaction { get; set; }
    }
}
