using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.Business.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.TotalCopies, opt => opt.MapFrom(src => src.Copies))
                .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.Copies));
            CreateMap<Book, BookResponseDto>();
        }
    }
}
