using MinimalApi.BookBasicExample.Entities;

namespace MinimalApi.BookBasicExample.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync( CancellationToken cancellationToken=default);

        Task<IEnumerable<Book>> SearchBooksNameAsync(string title, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(Book book, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Book book ,CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Book> GetByBookIdAsync(int id, CancellationToken cancellationToken = default);

    }
}
