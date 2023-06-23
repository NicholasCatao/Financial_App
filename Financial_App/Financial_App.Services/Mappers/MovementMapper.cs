using AutoMapper;
using Financial_App.Application.Interfaces;
using Financial_App.Domain.Model;
using Financial_App.Domain.Response;

namespace Financial_App.Services.Mappers
{
    public class MovementMapper : IMovementMapper
    {
        private readonly IMapper _mapper;
        public MovementMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MovementModel, MovementResponse>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<IEnumerable<MovementResponse>> MapModelToResponseAsync(IEnumerable<MovementModel> model)
        => _mapper.Map<IEnumerable<MovementModel>, IEnumerable<MovementResponse>>(model);
    }
}
