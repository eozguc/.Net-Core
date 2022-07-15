using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestProject1.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup", GenreId = 0, PageCount = 200, PublishDate = new DateTime(2001, 06, 12)
                },
                new Book { Title = "Herland", GenreId = 1, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
                new Book
                {
                    Title = "Frankestein", GenreId = 1, PageCount = 540, PublishDate = new DateTime(1981, 12, 21)
                },
                new Book
                {
                    Title = "Dune",GenreId = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21)
                }
            );
        }
    }
}