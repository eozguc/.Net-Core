using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public interface IMovieStoreDbContext
    {

        //DbSet<Person> Persons { get; set; }
        DbSet<Movie> Movies {get;set;}
        DbSet<Genre> Genres {get;set;}
        DbSet<Actor> Actors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<MovieActor> MovieActors { get; set; }
        DbSet<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }
        DbSet<OrderMovie> OrderMovies { get; set; }

        int SaveChanges();
    }
}