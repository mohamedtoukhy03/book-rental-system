using AutoMapper;
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Exceptions;
using BookRentalSystem.DataAccess.Models;
using BookRentalSystem.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace BookRentalSystem.Business.Services
{
    public class BookService(IBookRepository _bookRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IBookService
    {
        public async Task<BookResponseDto> AddBookAsync(CreateBookDto createBookDto)
        {
            var checkExistingBook = await _bookRepository.ExistsByIsbnAsync(createBookDto.ISBN);
            if (checkExistingBook)
            {
                throw new ConflictException($"A book with ISBN '{createBookDto.ISBN}' already exists.");
            }

            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookResponseDto>(book);
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }

        public async Task<IEnumerable<BookResponseDto>> GetAvailableBooksAsync()
        {
            var books = await _bookRepository.GetAvailableBooksAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }

        public async Task<BookResponseDto> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found.");
            }
            return _mapper.Map<BookResponseDto>(book);
        }
    }
}
