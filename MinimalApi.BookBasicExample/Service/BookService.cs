using Microsoft.EntityFrameworkCore;
using MinimalApi.BookBasicExample.Context;
using MinimalApi.BookBasicExample.Entities;

namespace MinimalApi.BookBasicExample.Service
{
    public sealed class BookService(ApplicationDbContext context) : IBookService
    {
        public async Task<bool> CreateAsync(Book book, CancellationToken cancellationToken = default)
        {
            await context.Books.AddAsync(book, cancellationToken);
           return await context.SaveChangesAsync(cancellationToken)>0;

        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Book book =await context.Books.FindAsync(id);
            if (book is null) return false;
            context.Books.Remove(book);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync( CancellationToken cancellationToken = default)
        {
          return  await context.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetByBookIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Books.FindAsync(id ,cancellationToken);
        }

      

        public async Task<IEnumerable<Book>> SearchBooksNameAsync(string title, CancellationToken cancellationToken = default)
        {
            return await context.Books.Where(b=>b.Title.Contains(title)).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book, CancellationToken cancellationToken = default)
        {
           context.Books.Update(book);
            return await context.SaveChangesAsync(cancellationToken)>0;
        }
    }
}
