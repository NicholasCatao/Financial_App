using Financial_App.Domain.Model;
using Financial_App.Domain.Request;
using Financial_App.InfraStructure.Data.Context;
using Financial_App.InfraStructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financial_App.InfraStructure.Repository.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;

        public AccountRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<AccountModel> GetAccountbyIdAsync(string accountId)
            => await _appDbContext.Accounts.FindAsync(accountId);

        public async Task<string> CreateAccountAsync(AccountModel accountModel)
        {
            await _appDbContext.AddAsync(accountModel);
            await _appDbContext.SaveChangesAsync();

            return accountModel.Id;
        }

        public async Task UpdateAccount(AccountModel accountModel)
        {
            _appDbContext.Accounts.Update(accountModel);
            await _appDbContext.SaveChangesAsync();
        }
           

        public async Task<IEnumerable<MovementModel>> AccountStatementbyRangeAsync(AccountFilter accountFilter)
            => await _appDbContext.Movements
                .Where(m => m.AccountId == accountFilter.AccountId && m.Data >= accountFilter.InitialDate && m.Data <= accountFilter.FinallDate).ToListAsync();

        public async Task<IEnumerable<MovementModel>> AccountStatementbyTypeAsync(AccountFilter accountFilter)
            => await _appDbContext.Movements
                .Where(m => m.Type == accountFilter.MovimentType).ToListAsync();

        public async Task<MovementModel> GetMovimentbyIdAsync(string movimentId)
            => await _appDbContext.Movements.FindAsync(movimentId);

        public async Task RegisterMovimentAsync(MovementModel movementModel)
        {
            await _appDbContext.AddAsync(movementModel);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
