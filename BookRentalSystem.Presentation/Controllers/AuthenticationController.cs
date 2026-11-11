
using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ResponseUserDto> RegisterUser(RegisterUserDto registerUserDto)
        {
            var result = await authenticationService.RegisterUserAsync(registerUserDto);
            return result; 
        }


    }
}
