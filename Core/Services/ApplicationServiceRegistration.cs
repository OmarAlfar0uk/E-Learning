using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using Share.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            services.Configure<MailSettings>(
                configuration.GetSection("MailSettings")
                );
            services.AddScoped<IMailServices, MailServices>();
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
