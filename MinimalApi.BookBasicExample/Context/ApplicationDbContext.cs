using Microsoft.EntityFrameworkCore;
using MinimalApi.BookBasicExample.Entities;

namespace MinimalApi.BookBasicExample.Context
{
    public sealed class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
