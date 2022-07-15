using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Güncellemeye çalıştığınız film türü bulunamadı!");
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimde bir film türü zaten mevcut!");

            genre.Name = !string.IsNullOrEmpty(Model.Name.Trim()) ? Model.Name : genre.Name;
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
    }
}