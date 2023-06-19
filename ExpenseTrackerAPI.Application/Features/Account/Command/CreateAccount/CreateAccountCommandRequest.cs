using ExpenseTrackerAPI.Application.Features.Common;
using ExpenseTrackerAPI.Application.Repositories;
using ExpenseTrackerAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ExpenseTrackerAPI.Application.Features.Account.Command.CreateAccount
{
    public class CreateAccountCommandRequest : CommonRequest, IRequest<CreateAccountCommandResponse>
    {
        public string AccountName { get; set; }
        public string CurrencyId { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommandRequest, CreateAccountCommandResponse>
    {
        readonly IAccountRepository _accountRepository;
        readonly ICurrencyRepository _currencyRepository;
        public CreateAccountCommandHandler(IAccountRepository accountRepository, ICurrencyRepository currencyRepository)
        {
            _accountRepository = accountRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<CreateAccountCommandResponse> Handle(CreateAccountCommandRequest request, CancellationToken cancellationToken)
        {

            Currency currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);

            E.Account account = new()
            {
                AccountNumber = GenerateDummyAccountNumber(),
                Balance = 0,
                Currency = currency,
                Name = request.AccountName,
                User = request.User
            };

            await _accountRepository.AddAsync(account);

            await _accountRepository.SaveAsync();

            return new()
            {
                Message = ResponseMessages.AccounCreatedSuccesfully.ToString(),
                Account = new {
                    account.Id,
                    account.AccountNumber,
                    account.Name,
                    Currency=account.Currency.Name,
                    account.Balance}
            };
        }



        private static Random random = new Random();

        public static string GenerateDummyAccountNumber()
        {
            const int accountNumberLength = 12;

            // Generate a random account number consisting of digits
            string accountNumber = string.Empty;
            for (int i = 0; i < accountNumberLength; i++)
            {
                accountNumber += random.Next(10).ToString();
            }

            return accountNumber;
        }
    }
}
