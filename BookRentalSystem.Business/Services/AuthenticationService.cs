using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Exceptions;
using BookRentalSystem.Business.Options;
using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.Business.Services
{
    public class AuthenticationService(
        UserManager<IdentityUser> userManager,
        IMapper mapper, 
        LibraryDbContext dbContext,
        IOptions<JwtOptions> jwtOptions
        ) : IAuthenticationService
    {
        public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var checkPassword = await userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!checkPassword)
            {
                throw new BadRequestException("Invalid email or password.");
            }  

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role == null)
            {
                throw new NotFoundException("User has no assigned role.");
            }

            return new LoginResponseDto
            {
                AccessToken = GenerateToken(user, role),
                ExpiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiresInMinutes),
                User = mapper.Map<UserResponseDto>(user)
            };
        }

        public async Task<UserResponseDto> RegisterAdminAsync(RegisterUserDto registerUserDto)
        {
            var user = await RegisterLogicAsync(registerUserDto, Role.Admin);
            return mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = await RegisterLogicAsync(registerUserDto, Role.User);
            return mapper.Map<UserResponseDto>(user);
        }

        private string GenerateToken(IdentityUser user, string? role)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(ClaimTypes.Role, role!)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Value.Key));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    jwtOptions.Value.ExpiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IdentityUser> RegisterLogicAsync(RegisterUserDto registerUserDto, string role)
        {
            var user = mapper.Map<IdentityUser>(registerUserDto);
            await using var transaction =
                await dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await userManager.CreateAsync(user, registerUserDto.Password);
                if (!result.Succeeded)
                {
                    throw new BadRequestException(
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                var roleResult = await userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    throw new BadRequestException(
                        string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }

                await transaction.CommitAsync();
                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
