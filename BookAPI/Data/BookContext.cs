using BookAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data
{
    public class BookContext: DbContext
    {
        public BookContext(DbContextOptions<BookContext> options): base(options)
        { }

        public DbSet<Book> Book => Set<Book>();
        public DbSet<Author> Author => Set<Author>();
        public DbSet<PublishingHouse> PublishingHouse => Set<PublishingHouse>();

    }
}
