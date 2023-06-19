using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ExpenseTrackerAPI.Application.Features.Account.Command.UpdateAccount
{
    public class UpdateAccountCommandRequest :CommonRequest, IRequest<UpdateAccountCommandResponse>
    {
        public string AccountId { get; set; }
        public int Balance { get; set; }
        public string AccountName { get; set; }
    }

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommandRequest, UpdateAccountCommandResponse>
    {
        readonly IAccountRepository _accountRepository;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UpdateAccountCommandResponse> Handle(UpdateAccountCommandRequest request, CancellationToken cancellationToken)
        {

            E.Account account = await _accountRepository.Table.Include(a => a.Currency).FirstOrDefaultAsync(a => a.Id == Guid.Parse(request.AccountId));
            if (account.User != request.User)
                throw new Exception("Access Denied");
            else if(request.Balance <= 0)
                return new() { IsSucceeded = false , Message=ResponseMessages.InvalidBalance.ToString()};
            else
            {
                account.Balance = request.Balance;
                account.Name = request.AccountName;
                await _accountRepository.SaveAsync();
            }

            return new()
            {
                IsSucceeded = true,
                Message = ResponseMessages.AccountUpdated.ToString(),
                Account = new
                {
                    account.Id,
                    account.AccountNumber,
                    account.Name,
                    Currency = account.Currency.Name,
                    account.Balance
                }
            };
        }
    }
}
