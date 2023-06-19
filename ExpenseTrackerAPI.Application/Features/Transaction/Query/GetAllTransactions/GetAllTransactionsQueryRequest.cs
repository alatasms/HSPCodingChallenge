using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Query.GetAllTransactions
{
    public class GetAllTransactionsQueryRequest : CommonRequest, IRequest<GetAllTransactionsQueryResponse>
    {
        public string AccountId { get; set; }
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQueryRequest, GetAllTransactionsQueryResponse>
    {
        readonly ITransactionRepository _transactionRepository;
        readonly IAccountRepository _accountRepository;
        public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<GetAllTransactionsQueryResponse> Handle(GetAllTransactionsQueryRequest request, CancellationToken cancellationToken)
        {

            var query = _transactionRepository.Table.Include(t => t.Account).ThenInclude(a=>a.User)
                .Include(t => t.Category)
                .Where(t => t.Account.Id == Guid.Parse(request.AccountId));

            if(query.FirstOrDefault() != null)
            if (!query.FirstOrDefault().Account.User.Equals(request.User))
                throw new Exception("Access Denied");


            var transactions = query.Select(t => new
            {
                t.Id,
                TransactionDate = t.TransactionDate.ToString("MM/dd/yyyy"),
                Spend= $"{t.Spend} {t.Account.Currency.Name}",
                Category = t.Category.Name
            }).ToList();


            return new()
            {
                Transactions = transactions
            };

        }
    }
}
