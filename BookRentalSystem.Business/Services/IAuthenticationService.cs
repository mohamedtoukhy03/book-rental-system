using BookRentalSystem.Business.DTOs;


namespace BookRentalSystem.Business.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseUserDto> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
