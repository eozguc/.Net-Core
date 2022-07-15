using BookStore.DBOperations;
using BookStore.Entities;

namespace TestProject1.TestSetup
{
    public static class Genres_
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre {Name = "Personal Growth"},
                new Genre {Name = "Science Fiction"},
                new Genre {Name = "Romance"}
            );
        }
    }
}