using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Exceptions;
using BookRentalSystem.Business.Options;
using BookRentalSystem.DataAccess.Models;
using BookRentalSystem.DataAccess.Repositories;
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
        IUnitOfWork unitOfWork,
        IOptions<JwtOptions> jwtOptions
        ) : IAuthenticationService
    {
        public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var checkPassword = await userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!checkPassword)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                throw new ForbiddenException("User has no assigned role.");
            }

            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(jwtOptions.Value.ExpiresInMinutes);

            return new LoginResponseDto
            {
                AccessToken = GenerateToken(user, roles, expiresAt),
                ExpiresAt = expiresAt,
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

        private string GenerateToken(IdentityUser user, IList<string> roles, DateTimeOffset expiresAt)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Value.Key));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                expires: expiresAt.UtcDateTime,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IdentityUser> RegisterLogicAsync(RegisterUserDto registerUserDto, string role)
        {
            var user = mapper.Map<IdentityUser>(registerUserDto);
            await using var transaction = await unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await userManager.CreateAsync(user, registerUserDto.Password);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Registration failed. Please check your username, email and password.");
                }

                var roleResult = await userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    throw new BadRequestException("Failed to assign a role to the user.");
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
