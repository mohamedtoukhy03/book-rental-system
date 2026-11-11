using AutoMapper;
using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.Business.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BookRentalSystem.DataAccess.Models;

namespace BookRentalSystem.Business.Services
{
    public class AuthenticationService(
        UserManager<IdentityUser> userManager,
        IMapper mapper
        ) : IAuthenticationService
    {
        public Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseUserDto> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var user = mapper.Map<IdentityUser>(registerUserDto);
            var result = await userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var roleResult = await userManager.AddToRoleAsync(user, Role.User);

            if (!roleResult.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            return mapper.Map<ResponseUserDto>(user);

        }
    }
}
