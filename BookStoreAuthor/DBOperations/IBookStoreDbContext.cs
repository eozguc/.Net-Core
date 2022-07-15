using BookStoreAuthor.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAuthor.DBOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }

        DbSet<Genre> Genres { get; set; }

        int SaveChanges();
    }
}