using BookRentalSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.Business.Services
{
    public interface IRentalRequestService
    {
        Task<RentalRequestResponseDto> CreateRequestAsync(string? userId, CreateRentalRequestDto dto);
        Task<IEnumerable<AdminRentalRequestResponseDto>> GetPendingRequestsAsync();
        Task<RentalRequestResponseDto> ApproveRequestAsync(Guid requestId, string? adminUserId);
        Task<RentalRequestResponseDto> RejectRequestAsync(Guid requestId, string? adminUserId);
        Task<IEnumerable<RentalRequestResponseDto>> GetUserRequestsAsync(string? userId);
    }
}
