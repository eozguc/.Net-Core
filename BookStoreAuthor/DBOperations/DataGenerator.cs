using System;
using System.Linq;
using BookStoreAuthor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreAuthor.DBOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context =
                new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());
            if (context.Books.Any()) return;

            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
            );

            context.Authors.AddRange(
                new Author
                {
                    FirstName = "Umberto",
                    LastName = "Eco",
                    DateOfBirth = new DateTime(1920, 10, 15)
                },
                new Author
                {
                    FirstName = "Hannah",
                    LastName = "Arendt",
                    DateOfBirth = new DateTime(1895, 6, 22)
                },
                new Author
                {
                    FirstName = "John",
                    LastName = "Steinbeck",
                    DateOfBirth = new DateTime(1902, 2, 27)
                }
            );

            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                });
            context.SaveChanges();
        }
    }
}