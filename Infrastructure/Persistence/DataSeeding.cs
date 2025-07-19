using Domain.Contract;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(E_LearnDbcontext _dbcontext,
        UserManager<AppUsers> _userManager,
        RoleManager<IdentityRole> _roleManager ,
        E_LearnIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task IdentityDataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbcontext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbcontext.Database.MigrateAsync();
                }

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new AppUsers()
                    {
                        Email = "omarAlfarouk@gamil.com",
                        DisplayName = "Omar Alfarouk",
                        PhoneNumber = "01226801925",
                        UserName = "OmarAlfarouk"
                    };

                    var User02 = new AppUsers()
                    {
                        Email = "MahmoudZain@gamil.com",
                        DisplayName = "Mahmoud Zain",
                        PhoneNumber = "01223783992",
                        UserName = "MahmoudZain"
                    };

                    await _userManager.CreateAsync(User01, "P@ss0rd");
                    await _userManager.CreateAsync(User02, "P@ss0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                
            }

        }
    }
}