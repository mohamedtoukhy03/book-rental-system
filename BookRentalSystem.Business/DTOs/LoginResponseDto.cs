namespace BookRentalSystem.Business.DTOs;

public record LoginResponseDto
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public UserResponseDto? User { get; init; }
}
