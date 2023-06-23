using Financial_App.Application.Interfaces;
using Financial_App.Application.Mappers;
using Financial_App.Domain.Interfaces;
using Financial_App.InfraStructure.Repository.Interfaces;
using Financial_App.InfraStructure.Repository.Repositories;
using Financial_App.Services;
using Financial_App.Services.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Financial_App.InfraStructure.Ioc
{
    public static class DepedencyInjection
    {
        public static void RegisterServicesInjection(this IServiceCollection services)
        {
            services.UseServices();
            services.UseRepositories();
            services.UserMappers();
        }

        public static void UseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void UseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void UserMappers(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMovementMapper), typeof(MovementMapper));
            services.AddScoped(typeof(IAccountMapper), typeof(AccountMapper));
        }
    }
}
