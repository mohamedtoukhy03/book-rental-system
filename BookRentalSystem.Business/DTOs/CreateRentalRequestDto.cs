using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRentalSystem.Business.DTOs
{
    public class CreateRentalRequestDto
    {
        [Required(ErrorMessage = "BookId is required")]
        public required Guid BookId { get; init; }
    }
}
