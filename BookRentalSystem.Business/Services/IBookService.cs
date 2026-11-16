using BookRentalSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.Business.Services
{
    public interface IBookService
    {
        Task<BookResponseDto> AddBookAsync(CreateBookDto createBookDto);
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<IEnumerable<BookResponseDto>> GetAvailableBooksAsync();
        Task<BookResponseDto> GetBookByIdAsync(Guid id);
    }
}
