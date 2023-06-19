using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Application.Features.Account.Query.GetAccounts
{
    public class GetAccountsQueryRequest : CommonRequest, IRequest<GetAccountsQueryResponse>
    {

    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQueryRequest, GetAccountsQueryResponse>
    {
        readonly IAccountRepository _accountRepository;

        public GetAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<GetAccountsQueryResponse> Handle(GetAccountsQueryRequest request, CancellationToken cancellationToken)
        {
            var accounts = _accountRepository.GetWhere(a => a.User == request.User).Include(a=>a.Currency)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.AccountNumber,
                    Currency = a.Currency.Name,
                    a.Balance
                }).ToList();

            return new()
            {
                Accounts = accounts,
            };

        }
    }
}
