using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Presentation.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/books")]
    [Authorize(Roles = $"{Role.Admin},{Role.SuperAdmin}")]
    public class AdminBookController(IBookService bookService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<BookResponseDto>> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var result = await bookService.AddBookAsync(createBookDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
