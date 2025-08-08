using CloudinaryDotNet;
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
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IModuleServices, ModuleServices>();
            services.AddScoped<ILessonServices, LessonServices>();
            services.AddScoped<IQuizServices, QuizServices>();
            services.AddScoped<IMailServices, MailServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IEnrollmentServices, EnrollmentServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IProgressServices, ProgressServices>();
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var cloudSettings = config.GetSection("CloudinarySettings").Get<CloudinarySettings>();
                var account = new Account(cloudSettings.CloudName, cloudSettings.ApiKey, cloudSettings.ApiSecret);
                return new CloudinaryDotNet.Cloudinary(account);
            });

            services.AddScoped<IOrederService, OrderService>();
            services.AddScoped<Func<IOrederService>>(provider =>
            () => provider.GetRequiredService<IOrederService>());
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
