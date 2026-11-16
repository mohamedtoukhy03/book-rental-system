using BookRentalSystem.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public class UnitOfWork(LibraryDbContext _dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}
