using Core.Business.Service;
using Core.Contracts.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Resolver.Register
{
    public static class IoCRegisterService
    {
        public static IServiceCollection AddRegistration(IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
