using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")] 
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await authenticationService.RegisterUserAsync(registerUserDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var result = await authenticationService.LoginUserAsync(loginUserDto);
            return Ok(result);
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = Role.SuperAdmin)]
        public async Task<ActionResult<UserResponseDto>> RegisterAdmin([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await authenticationService.RegisterAdminAsync(registerUserDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
