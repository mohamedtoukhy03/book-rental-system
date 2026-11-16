using AutoMapper;
using BookRentalSystem.Business.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.Business.Profiles
{

    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterUserDto, IdentityUser>();
            CreateMap<IdentityUser, UserResponseDto>();
        }
    }
}
