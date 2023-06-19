using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;

namespace ExpenseTrackerAPI.Application.Features.Account.Command.RemoveAccount
{
    public class RemoveAccountCommandRequest : CommonRequest, IRequest<RemoveAccountCommandResponse>
    {
        public string Id { get; set; }
    }

    public class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommandRequest, RemoveAccountCommandResponse>
    {
        readonly IAccountRepository _accountRepository;
        public RemoveAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<RemoveAccountCommandResponse> Handle(RemoveAccountCommandRequest request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.Id);
            if (account.User != request.User)
            {
                throw new Exception("Access Denied");
            }
            else
            {
                var result = await _accountRepository.RemoveAsync(request.Id);
                await _accountRepository.SaveAsync();

                return new() { IsSucceeded = result, Message = result ? ResponseMessages.AccountDeleted.ToString() : "" };
            }
            
        }
    }
}
