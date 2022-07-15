using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }

        DbSet<Genre> Genres { get; set; }

        int SaveChanges();
    }
}