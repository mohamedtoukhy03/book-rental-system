namespace BookRentalSystem.Business.DTOs;

public class LoginResponseDto
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required ResponseUserDto User { get; init; }
}
