using Financial_App.Domain.Model;
using Financial_App.Domain.Request;
using Financial_App.Domain.Response;

namespace Financial_App.Domain.Interfaces
{
    public interface IAccountMapper
    {
        Task<AccountResponse> MapModelToResponseAsync(AccountModel model);
        Task<AccountModel> MapRequestToModelAsync(AccountRequest request);
    }
}
