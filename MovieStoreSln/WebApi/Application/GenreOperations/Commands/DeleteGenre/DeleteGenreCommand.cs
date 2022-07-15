using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    { 
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Handle(){
            var genre = _dbContext.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(genre is null)
                throw new InvalidOperationException("Silmeye çalıştığınız film türü bulunamadı!");
            var movies = _dbContext.Movies.Any(x=>x.GenreId==GenreId);
            if(!movies)
                throw new InvalidOperationException("Önce film türünüe ait filmler silinmelidir!");
            _dbContext.Genres.Remove(genre);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}