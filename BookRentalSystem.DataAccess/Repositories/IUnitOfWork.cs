using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
