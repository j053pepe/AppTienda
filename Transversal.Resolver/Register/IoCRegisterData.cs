using Core.Contracts.Data;
using Infraestructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Transversal.Resolver.Register
{
    public static class IoCRegisterData
    {
        public static IServiceCollection AddRegistration(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
