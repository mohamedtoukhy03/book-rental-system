using Microsoft.AspNetCore.Identity;
using System;

namespace BookRentalSystem.DataAccess.Models
{
    public class RentalRequest
    {
        public Guid Id { get; set; }
        public required Guid BookId { get; set; }
        public required string UserId { get; set; }
        public string? ReviewedByUserId { get; set; }
        public RentalRequestStatus Status { get; set; } = RentalRequestStatus.Pending;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public Book Book { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        public IdentityUser? Admin { get; set; }
    }
}
