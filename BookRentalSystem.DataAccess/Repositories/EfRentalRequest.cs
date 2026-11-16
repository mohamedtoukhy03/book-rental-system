using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public class EfRentalRequest(LibraryDbContext _dbContext) : IRentalRequestRepository
    {
        public async Task AddAsync(RentalRequest rentalRequest)
        {
            await _dbContext.RentalRequests.AddAsync(rentalRequest);
        }

        public async Task<RentalRequest?> GetByIdAsync(Guid id)
        {
            var rentalRequest = await _dbContext.RentalRequests
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
            return rentalRequest;
        }

        public async Task<IEnumerable<RentalRequest>> GetPendingRequestsAsync()
        {
            var requests = await _dbContext.RentalRequests
                .AsNoTracking()
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.Status == RentalRequestStatus.Pending)
                .OrderBy(r => r.RequestedAt)
                .ToListAsync();
            return requests;
        }

        public async Task<IEnumerable<RentalRequest>> GetUserRequestsAsync(string userId)
        {
            var requests = await _dbContext.RentalRequests
                .AsNoTracking()
                .Include(r => r.Book)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();
            return requests;
        }

        public async Task<bool> HasPendingRequestAsync(string userId, Guid bookId)
        {
            var hasPendingRequest = await _dbContext.RentalRequests.AnyAsync(
                r => r.BookId == bookId
                && r.UserId == userId
                && r.Status == RentalRequestStatus.Pending
            );
            return hasPendingRequest;
        }
    }
}
