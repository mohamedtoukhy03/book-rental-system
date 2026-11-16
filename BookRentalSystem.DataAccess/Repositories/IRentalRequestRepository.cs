using BookRentalSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public interface IRentalRequestRepository
    {
        public Task AddAsync(RentalRequest rentalRequest);
        public Task<RentalRequest?> GetByIdAsync(Guid id);
        public Task<IEnumerable<RentalRequest>> GetAllAsync();
        public Task<IEnumerable<RentalRequest>> GetPendingRequestsAsync();
        public Task<IEnumerable<RentalRequest>> GetUserRequestsAsync(string userId);
        public Task<bool> HasPendingRequestAsync(string userId, Guid bookId);
        public Task SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
