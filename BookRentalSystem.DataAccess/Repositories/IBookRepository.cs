using BookRentalSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book?> GetByIdWithLockAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<bool> ExistsByIsbnAsync(string isbn);
    }
}
