using BookRentalSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public interface IBookRepository
    {
        public Task AddAsync(Book book);
        public Task<Book?> GetByIdAsync(Guid id);
        public Task<Book?> GetByIdWithLockAsync(Guid id);
        public Task<IEnumerable<Book>> GetAllAsync();
        public Task<IEnumerable<Book>> GetAvailableBooksAsync();
        public Task<bool> ExistsByIsbnAsync(string isbn);
        public Task SaveChangesAsync();
    }
}
