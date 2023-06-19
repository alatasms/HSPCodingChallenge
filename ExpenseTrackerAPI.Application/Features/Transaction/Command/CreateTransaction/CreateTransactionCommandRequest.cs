global using E = ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using ExpenseTrackerAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Command.CreateTransaction
{
    public class CreateTransactionCommandRequest :CommonRequest, IRequest<CreateTransactionCommandResponse>
    {
        public int SpendAmount { get; set; }
        public string CategoryId { get; set; }
        public string AccountId { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommandRequest, CreateTransactionCommandResponse>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IAccountRepository _accountRepository;
        readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ICategoryRepository categoryRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<CreateTransactionCommandResponse> Handle(CreateTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            E.Account account = await _accountRepository.Table.Include(a=>a.Currency).FirstOrDefaultAsync(a=>a.Id==Guid.Parse(request.AccountId));
            if (!account.User.Equals(request.User))
                throw new Exception(ResponseMessages.AccessDenied.ToString());

            Category category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (account.Balance < request.SpendAmount || request.SpendAmount<=0)
            {
                return new() { IsSucceeded = false, Message = ResponseMessages.InsufficientBalance.ToString()};
            }

            E.Transaction transaction = new()
            {
                Account = account,
                Category = category,
                Spend = request.SpendAmount,
                TransactionDate = DateTime.UtcNow
            };
            account.Balance -= transaction.Spend;
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveAsync();
            return new()
            {
                Message = ResponseMessages.TransactionUpdated.ToString(),
                Transaction =new
                {
                    transaction.Id,
                    TransactionDate = transaction.TransactionDate.ToString("MM/dd/yyyy"),
                    Spend = $"{transaction.Spend} {transaction.Account.Currency.Name}",
                    Category = transaction.Category.Name
                }
            };

        }
    }
}
