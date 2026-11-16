using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.Business.DTOs
{
    public class RentalRequestResponseDto
    {
        public Guid Id { get; init; }
        public required string Status { get; init; }
        public DateTime RequestedAt { get; init; }
        public string? ReviewedByUserId { get; init; }
        public BookResponseDto? Book { get; init; }
    }
}
