using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.Presentation.Controllers.User
{
    [ApiController]
    [Route("api/user/books")]
    public class BookBrowsingController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAllBooks()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAvailableBooks()
        {
            var books = await bookService.GetAvailableBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDto>> GetBookById(Guid id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
    }
}
