using BookRentalSystem.Business.Profiles;
using BookRentalSystem.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookRentalSystem.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AuthenticationProfile).Assembly);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IRentalRequestService, RentalRequestService>();

            return services;
        }
    }
}
