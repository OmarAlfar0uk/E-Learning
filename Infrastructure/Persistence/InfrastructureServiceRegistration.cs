

using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;

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

            return Services;
        }
    }
}
