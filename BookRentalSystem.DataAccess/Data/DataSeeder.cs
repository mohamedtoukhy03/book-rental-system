using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            var existingSuperAdmins = await userManager.GetUsersInRoleAsync(Role.SuperAdmin);
            if (existingSuperAdmins.Count > 0)
            {
                return;
            }

            var configuration = services.GetRequiredService<IConfiguration>();
            var superAdminEmail = configuration["SuperAdmin:Email"];
            var superAdminPassword = configuration["SuperAdmin:Password"];

            if (string.IsNullOrWhiteSpace(superAdminEmail) || string.IsNullOrWhiteSpace(superAdminPassword))
            {
                throw new InvalidOperationException("Super admin email or password is missing.");
            }

            if (await userManager.FindByEmailAsync(superAdminEmail) != null)
            {
                throw new InvalidOperationException("Super admin email is already in use.");
            }

            var superAdminUser = new IdentityUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(superAdminUser, superAdminPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create the super admin.");
            }

            var roleResult = await userManager.AddToRoleAsync(superAdminUser, Role.SuperAdmin);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to assign the SuperAdmin role.");
            }
        }
    }
}
