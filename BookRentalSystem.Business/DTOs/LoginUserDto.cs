using System.ComponentModel.DataAnnotations;

namespace BookRentalSystem.Business.DTOs;

public record LoginUserDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
