using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.Business.DTOs
{
    public record BookResponseDto
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Author { get; init; }
        public required string ISBN { get; init; }
        public int TotalCopies { get; init; }
        public int AvailableCopies { get; init; }
    }
}
