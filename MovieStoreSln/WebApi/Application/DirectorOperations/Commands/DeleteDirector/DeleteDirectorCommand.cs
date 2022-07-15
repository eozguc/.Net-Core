using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);
            if(director is null)
                throw new InvalidOperationException("Silmeye çalıştığınız yönetmen bulunamadı!");
            var movies = _dbContext.Movies.Any(x=> x.DirectorId == DirectorId);
            if(!movies)
                throw new InvalidOperationException("Önce filmden yönetmen silinmelidir!");
            _dbContext.Directors.Remove(director);            
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}