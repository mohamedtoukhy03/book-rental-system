using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRentalSystem.Business.DTOs
{
    public record CreateBookDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200)]
        public required string Title { get; init; }
        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100)]
        public required string Author { get; init; }
        [Required(ErrorMessage = "ISBN is required")]
        [MaxLength(20)]
        public required string ISBN { get; init; }
        [Required(ErrorMessage = "Copies count is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Copies must be a positive integer greater than zero")]
        public required int Copies { get; init; }
    }
}
