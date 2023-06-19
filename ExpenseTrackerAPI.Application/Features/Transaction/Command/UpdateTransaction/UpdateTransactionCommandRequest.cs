using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using ExpenseTrackerAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Command.UpdateTransaction
{
    public class UpdateTransactionCommandRequest :CommonRequest , IRequest<UpdateTransactionCommandResponse>
    {
        public string Id { get; set; }
        public int SpendAmount { get; set; }
        public string CategoryId { get; set; }
    }


    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommandRequest, UpdateTransactionCommandResponse>
    {

        readonly ITransactionRepository _transactionRepository;
        readonly ICategoryRepository _categoryRepository;
        public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateTransactionCommandResponse> Handle(UpdateTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.Table.Include(t => t.Account).ThenInclude(a=>a.Currency)
                .Include(t => t.Category)
                .Where(t => t.Id == Guid.Parse(request.Id))
                .FirstOrDefaultAsync();

            if(!transaction.Account.User.Equals(request.User))
                throw new Exception(ResponseMessages.AccessDenied.ToString());
            if (transaction.Account.Balance < request.SpendAmount || request.SpendAmount <= 0)
            {
                return new() { IsSucceeded = false, Message = ResponseMessages.InsufficientBalance.ToString() };
            }
            transaction.Account.Balance += (transaction.Spend - request.SpendAmount);
            transaction.Spend = request.SpendAmount;
            transaction.Category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            await _transactionRepository.SaveAsync();

            return new()
            {
                Message = ResponseMessages.TransactionUpdated.ToString(),
                Transaction = new
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
