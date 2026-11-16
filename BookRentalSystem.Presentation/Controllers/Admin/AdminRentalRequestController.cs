using BookRentalSystem.Business.DTOs;
using BookRentalSystem.Business.Services;
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookRentalSystem.Presentation.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/rental-requests")]
    [Authorize(Roles = $"{Role.Admin},{Role.SuperAdmin}")]
    public class AdminRentalRequestController(IRentalRequestService rentalRequestService) : ControllerBase
    {
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<AdminRentalRequestResponseDto>>> GetPendingRequests()
        {
            var requests = await rentalRequestService.GetPendingRequestsAsync();
            return Ok(requests);
        }

        [HttpPatch("{id}/approve")]
        public async Task<ActionResult<RentalRequestResponseDto>> ApproveRequest(Guid id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await rentalRequestService.ApproveRequestAsync(id, adminId);
            return Ok(result);
        }

        [HttpPatch("{id}/reject")]
        public async Task<ActionResult<RentalRequestResponseDto>> RejectRequest(Guid id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await rentalRequestService.RejectRequestAsync(id, adminId);
            return Ok(result);
        }
    }
}
