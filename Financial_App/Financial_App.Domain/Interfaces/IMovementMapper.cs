using Financial_App.Domain.Model;
using Financial_App.Domain.Response;

namespace Financial_App.Application.Interfaces
{
    public interface IMovementMapper
    {
        Task<IEnumerable<MovementResponse>> MapModelToResponseAsync(IEnumerable<MovementModel> model);
    }
}
