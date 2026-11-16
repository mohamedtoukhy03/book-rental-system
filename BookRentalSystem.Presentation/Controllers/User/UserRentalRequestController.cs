using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookRentalSystem.Presentation.Controllers.User
{
    [ApiController]
    [Route("api/user/rental-requests")]
    [Authorize]
    public class UserRentalRequestController(IRentalRequestService rentalRequestService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<RentalRequestResponseDto>> RequestRental([FromBody] CreateRentalRequestDto createRentalRequestDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await rentalRequestService.CreateRequestAsync(userId, createRentalRequestDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<RentalRequestResponseDto>>> GetMyRentalRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await rentalRequestService.GetUserRequestsAsync(userId);
            return Ok(requests);
        }
    }
}
