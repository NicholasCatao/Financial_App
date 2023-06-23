using AutoMapper;
using Financial_App.Domain.Interfaces;
using Financial_App.Domain.Model;
using Financial_App.Domain.Request;
using Financial_App.Domain.Response;

namespace Financial_App.Application.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountRequest, AccountModel>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<AccountResponse> MapModelToResponseAsync(AccountModel model)
            => _mapper.Map<AccountModel, AccountResponse>(model);
        public async Task<AccountModel> MapRequestToModelAsync(AccountRequest request)
         => _mapper.Map<AccountRequest, AccountModel>(request);
    }
}
