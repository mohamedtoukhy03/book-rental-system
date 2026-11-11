using Microsoft.EntityFrameworkCore;
using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await DataSeeder.SeedAsync(services);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapGet("/openapi/contract.yaml", async context =>
    {
        context.Response.ContentType = "application/yaml";
        await context.Response.SendFileAsync(
            Path.Combine(app.Environment.ContentRootPath, "OpenApi", "book-rental-api-contract.yaml"));
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/contract.yaml", "Book Rental System API Contract");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
