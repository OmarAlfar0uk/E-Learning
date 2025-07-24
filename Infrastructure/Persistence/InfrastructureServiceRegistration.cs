

using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services;
using ServicesAbstraction;
using System.Text;

namespace Persistence
{
     public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<E_LearnDbcontext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddDbContext<E_LearnIdentityDbContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("IdentityConnection")));

            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddIdentityCore<AppUsers>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<E_LearnIdentityDbContext>();

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            }).AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                var googleSection = Configuration.GetSection("Authentication:Google");
                options.ClientId = googleSection["ClientId"];
                options.ClientSecret = googleSection["ClientSecret"];
                options.SaveTokens = true;
            });


            return Services;
        }
    }
}
