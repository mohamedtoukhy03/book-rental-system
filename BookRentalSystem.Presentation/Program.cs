using BookRentalSystem.Business.Options;
using BookRentalSystem.Business.Profiles;
using BookRentalSystem.Business.Services;
using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Models;
using BookRentalSystem.DataAccess.Repositories;
using BookRentalSystem.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(AuthenticationProfile).Assembly);

builder.Services.AddIdentityCore<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>();

builder.Services.AddScoped<IBookRepository, EfBook>();
builder.Services.AddScoped<IRentalRequestRepository, EfRentalRequest>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRentalRequestService, RentalRequestService>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>();
if (jwtSettings == null || string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException("JWT settings are not configured properly.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(Role.Admin, Role.SuperAdmin));
    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole(Role.SuperAdmin));
    options.AddPolicy("UserOnly", policy => policy.RequireRole(Role.User));
});

var app = builder.Build();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedAsync(services);
}

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
