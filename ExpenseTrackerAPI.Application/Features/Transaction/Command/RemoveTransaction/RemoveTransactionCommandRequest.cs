using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Command.RemoveTransaction
{
    public class RemoveTransactionCommandRequest :CommonRequest, IRequest<RemoveTransactionCommandResponse>
    {
        public string Id { get; set; }
    }
    public class RemoveTransactionCommandHandler : IRequestHandler<RemoveTransactionCommandRequest, RemoveTransactionCommandResponse>
    {
        readonly ITransactionRepository _transactionRepository;

        public RemoveTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<RemoveTransactionCommandResponse> Handle(RemoveTransactionCommandRequest request, CancellationToken cancellationToken)
        {

            var transaction = _transactionRepository.Table.Include(t=>t.Account)
                .Where(t=>t.Id == Guid.Parse(request.Id)).FirstOrDefault();

            if (!transaction.Account.User.Equals(request.User))
                throw new Exception(ResponseMessages.AccessDenied.ToString());

            transaction.Account.Balance += transaction.Spend;

            var result = await _transactionRepository.RemoveAsync(request.Id);
            await _transactionRepository.SaveAsync();

            return new() { IsSucceeded = result, Message = result ? ResponseMessages.TransactionDeleted.ToString() : "" };
        }
    }
}
