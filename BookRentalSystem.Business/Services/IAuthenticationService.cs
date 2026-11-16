using BookRentalSystem.Business.DTOs;


namespace BookRentalSystem.Business.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponseDto> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
        Task<UserResponseDto> RegisterAdminAsync(RegisterUserDto registerUserDto);
    }
}
