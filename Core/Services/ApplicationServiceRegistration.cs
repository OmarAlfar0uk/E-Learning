using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
