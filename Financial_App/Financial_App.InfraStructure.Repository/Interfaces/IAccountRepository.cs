using Financial_App.Domain.Model;
using Financial_App.Domain.Request;

namespace Financial_App.InfraStructure.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountModel> GetAccountbyIdAsync(string accountId);
        Task<string> CreateAccountAsync(AccountModel accountModel);
        Task UpdateAccount(AccountModel accountModel);
        Task RegisterMovimentAsync(MovementModel movementModel);
        Task<MovementModel> GetMovimentbyIdAsync(string movimentId);
        Task<IEnumerable<MovementModel>> AccountStatementbyRangeAsync(AccountFilter accountFilter);
        Task<IEnumerable<MovementModel>> AccountStatementbyTypeAsync(AccountFilter accountFilter);
    }
}
