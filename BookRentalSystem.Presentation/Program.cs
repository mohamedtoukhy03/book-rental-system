using BookRentalSystem.Business;
using BookRentalSystem.DataAccess;
using BookRentalSystem.Presentation.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDataAccess(builder.Configuration)
    .AddBusiness()
    .AddPresentation()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies();

var app = builder.Build();

app.UseExceptionHandler();

await app.SeedDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
