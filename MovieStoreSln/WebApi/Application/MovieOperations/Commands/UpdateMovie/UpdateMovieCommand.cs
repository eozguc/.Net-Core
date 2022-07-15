using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public int MovieId { get; set; }
        public UpdateMovieViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var movie = _dbContext.Movies.SingleOrDefault(a => a.Id == MovieId);
            if(movie is null)
                throw new InvalidOperationException("Güncellemeye çalıştığınız film bulunamadı!");
            movie.Title = Model.Title == default ? movie.Title : Model.Title;
            movie.ReleaseDate = Model.ReleaseDate == default ? movie.ReleaseDate : Model.ReleaseDate;
            movie.GenreId = Model.GenreId == default ? movie.GenreId : Model.GenreId;
            movie.DirectorId = Model.DirectorId == default ? movie.DirectorId : Model.DirectorId;
            movie.Price = Model.Price == default ? movie.Price : Model.Price;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateMovieViewModel
    {
        public string Title { get; set; }
        
        public int ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }  
    }
}