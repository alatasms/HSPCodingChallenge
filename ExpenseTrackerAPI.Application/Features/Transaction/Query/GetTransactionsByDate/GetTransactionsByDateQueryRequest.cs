using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.Transaction.Query.GetTransactionsByDate
{
    public class GetTransactionsByDateQueryRequest :CommonRequest, IRequest<GetTransactionsByDateQueryResponse>
    {
        public string AccountId { get; set; }
        public string Date { get; set; }
    }

    public class GetTransactionsByDateQueryHandler : IRequestHandler<GetTransactionsByDateQueryRequest, GetTransactionsByDateQueryResponse>
    {
        readonly ITransactionRepository _transactionRepository;

        public GetTransactionsByDateQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<GetTransactionsByDateQueryResponse> Handle(GetTransactionsByDateQueryRequest request, CancellationToken cancellationToken)
        {

            DateTime date = DateTime.ParseExact(request.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            var query = _transactionRepository.Table.Include(t => t.Account).ThenInclude(a=>a.User)
                .Include(t => t.Category)
                .Where(t => t.Account.Id == Guid.Parse(request.AccountId) && t.TransactionDate.Date.Equals(date.Date));



            if (query.FirstOrDefault() != null)
                if (!query.FirstOrDefault().Account.User.Equals(request.User))
                    throw new Exception("Access Denied");

            var transactions = query.Select(t => new
            {
                t.Id,
                TransactionDate = t.TransactionDate.ToString("MM/dd/yyyy"),
                Spend = $"{t.Spend} {t.Account.Currency.Name}",
                Category = t.Category.Name
            }).ToList();

            return new()
            {
                Transactions = transactions
            };
        }
    }
}
