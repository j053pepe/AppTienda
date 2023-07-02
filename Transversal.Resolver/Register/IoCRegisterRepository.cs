using Core.Contracts.Repositories;
using Infraestructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Transversal.Resolver.Register
{
    public static class IoCRegisterRepository
    {
        public static IServiceCollection AddRegistration(IServiceCollection services)
        {
            services.AddTransient<IProductoRepository, IProductoRepository>();
            services.AddTransient<TiendaRepository, TiendaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVentaDetalleRepository, IVentaDetalleRepository>();
            services.AddTransient<IVentaRepository, IVentaRepository>();

            return services;
        }
    }
}
