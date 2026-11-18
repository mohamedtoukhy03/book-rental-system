using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookRentalSystem.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<IdentityUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>();

            services.AddScoped<IBookRepository, EfBook>();
            services.AddScoped<IRentalRequestRepository, EfRentalRequest>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
