using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRentalSystem.Business.DTOs
{
    public record CreateRentalRequestDto
    {
        [Required(ErrorMessage = "BookId is required")]
        public required Guid BookId { get; init; }
    }
}
