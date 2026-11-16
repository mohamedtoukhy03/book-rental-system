using BookRentalSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public interface IRentalRequestRepository
    {
        Task AddAsync(RentalRequest rentalRequest);
        Task<RentalRequest?> GetByIdAsync(Guid id);
        Task<IEnumerable<RentalRequest>> GetPendingRequestsAsync();
        Task<IEnumerable<RentalRequest>> GetUserRequestsAsync(string userId);
        Task<bool> HasPendingRequestAsync(string userId, Guid bookId);
    }
}
