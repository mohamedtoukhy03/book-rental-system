using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.Business.Mappers
{
    public class RentalRequestProfile : Profile
    {
        public RentalRequestProfile()
        {
            CreateMap<RentalRequest, RentalRequestResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            CreateMap<RentalRequest, AdminRentalRequestResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
