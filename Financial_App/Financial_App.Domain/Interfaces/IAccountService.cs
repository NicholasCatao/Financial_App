using Financial_App.Domain.Request;
using Financial_App.Domain.Response;

namespace Financial_App.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AccountResponse>> GetAccountbyIdAsync(string accountId);
        Task<Response<AccountResponse>> CreateAccountAsync(AccountRequest request);
        Task<Response<AccountResponse>> AccountDropCashAsync(string accountId, decimal amount);
        Task<Response<AccountResponse>> AccountMoveFundsAsync(MoveFundsRequest moveFundsRequest);

        Task<Response<IEnumerable<MovementResponse>>> AccountStatementbyRangeAsync(AccountFilter accountFilter);
        Task<Response<IEnumerable<MovementResponse>>> AccountStatementbyTypeAsync(AccountFilter accountFilter);
    }
}
