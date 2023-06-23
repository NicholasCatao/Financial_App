using Financial_App.Application.Interfaces;
using Financial_App.Domain.Enums;
using Financial_App.Domain.Interfaces;
using Financial_App.Domain.Model;
using Financial_App.Domain.Request;
using Financial_App.Domain.Response;
using Financial_App.InfraStructure.Repository.Interfaces;

namespace Financial_App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMapper _accountMapper;
        private readonly IMovementMapper _movementMapper;

        public AccountService(IAccountRepository accountRepository, IAccountMapper accountMapper, IMovementMapper movementMapper)
        {
            _accountRepository = accountRepository;
            _accountMapper = accountMapper;
            _movementMapper = movementMapper;
        }

        public async Task<Response<AccountResponse>> GetAccountbyIdAsync(string accountId)
        {
            var model = await _accountRepository.GetAccountbyIdAsync(accountId);
            if (model is null) return new Response<AccountResponse>(Domain.Enums.MotivoErro.NotFound, "Account not Found.");

            return new Response<AccountResponse>(await _accountMapper.MapModelToResponseAsync(model));
        }

        public async Task<Response<AccountResponse>> CreateAccountAsync(AccountRequest request)
        {
            var model = await _accountMapper.MapRequestToModelAsync(request);
            var id = await _accountRepository.CreateAccountAsync(model);

            return await GetAccountbyIdAsync(id);
        }

        public async Task<Response<AccountResponse>> AccountDropCashAsync(string accountId, decimal amount)
        {
            var model = await _accountRepository.GetAccountbyIdAsync(accountId);
            model.Balance += amount;

           await _accountRepository.UpdateAccount(model);

            return await GetAccountbyIdAsync(model.Id);
        }

        public async Task<Response<AccountResponse>> AccountMoveFundsAsync(MoveFundsRequest moveFundsRequest)
        {
            var fromAccount = await _accountRepository.GetAccountbyIdAsync(moveFundsRequest.AccountIdSource);
            var toAccount = await _accountRepository.GetAccountbyIdAsync(moveFundsRequest.AccountIdDestination);

            if (fromAccount is null) // add swtich expression
                return new Response<AccountResponse>(MotivoErro.NotFound, "Account source not found");

            if (toAccount is null) 
                return new Response<AccountResponse>(MotivoErro.NotFound, "Account destination not found");

            return await TransferAsync(fromAccount, toAccount, moveFundsRequest.FundAmount);
        }

        public async Task<Response<IEnumerable<MovementResponse>>> AccountStatementbyRangeAsync(AccountFilter accountFilter)
        {
            var data =  await _movementMapper.MapModelToResponseAsync(await _accountRepository.AccountStatementbyRangeAsync(accountFilter));
            return new Response<IEnumerable<MovementResponse>>(data);
        }

        public async Task<Response<IEnumerable<MovementResponse>>> AccountStatementbyTypeAsync(AccountFilter accountFilter)
        {
            var data = await _movementMapper.MapModelToResponseAsync(await _accountRepository.AccountStatementbyTypeAsync(accountFilter));
            return new Response<IEnumerable<MovementResponse>>(data);
        }

        private async Task<Response<AccountResponse>> TransferAsync(AccountModel fromAccount, AccountModel toAccount, decimal amount)
        {
            if (fromAccount.Balance >= amount)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;

               await _accountRepository.UpdateAccount(fromAccount);

                await _accountRepository.RegisterMovimentAsync(new MovementModel
                {
                    AccountId = fromAccount.Id,
                    Data = DateTime.UtcNow,
                    Amount = amount,
                    Description = $"Tranfer from {fromAccount.Id} to {toAccount.Id}",
                    Type = MovimentType.Debit
                });

               await _accountRepository.UpdateAccount(toAccount);

                await _accountRepository.RegisterMovimentAsync(new MovementModel
                {
                    AccountId = toAccount.Id,
                    Data = DateTime.UtcNow,
                    Amount = amount,
                    Description = $"Tranfer from {fromAccount.Id} to {toAccount.Id}",
                    Type = MovimentType.Credit
                });

                return await GetAccountbyIdAsync(fromAccount.Id);
            }
            else
            {
                return new Response<AccountResponse>(Domain.Enums.MotivoErro.BadRequest, $"Transfer failed. Account {fromAccount.Id} has insufficient funds.");
            }
        }
    }
}
