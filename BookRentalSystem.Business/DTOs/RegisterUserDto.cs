using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Business.DTOs
{
    public record RegisterUserDto
    {
        [Required]
        [MaxLength(50)]
        public required string UserName { get; init; }
        [Required]
        [EmailAddress]
        public required string Email { get; init; }
        [Required]
        public required string Password { get; init; }
    }
}
