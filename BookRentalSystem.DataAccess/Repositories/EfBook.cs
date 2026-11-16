using BookRentalSystem.DataAccess.Data;
using BookRentalSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentalSystem.DataAccess.Repositories
{
    public class EfBook(LibraryDbContext _dbContext) : IBookRepository
    {
        public async Task AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _dbContext.Books.AsNoTracking().ToListAsync();
            return books;
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task<Book?> GetByIdWithLockAsync(Guid id)
        {
            var book = await _dbContext.Books
                .FromSqlInterpolated($"SELECT * FROM \"Books\" WHERE \"Id\" = {id} FOR UPDATE")
                .FirstOrDefaultAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            var books = await _dbContext.Books
                .AsNoTracking()
                .Where(b => b.AvailableCopies > 0)
                .ToListAsync();
            return books;
        }

        public async Task<bool> ExistsByIsbnAsync(string isbn)
        {
            var exists = await _dbContext.Books.AnyAsync(b => b.ISBN == isbn);
            return exists;
        }
    }
}
