using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Resolver.Register;

namespace Transversal.Resolver
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(IServiceCollection services)
        {
            IoCRegisterData.AddRegistration(services);
            IoCRegisterRepository.AddRegistration(services);
            IoCRegisterService.AddRegistration(services);
            return services;
        }
    }
}
