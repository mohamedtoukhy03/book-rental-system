using BookRentalSystem.DataAccess.Data;

namespace BookRentalSystem.Presentation.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            await DataSeeder.SeedAsync(scope.ServiceProvider);
        }
    }
}
