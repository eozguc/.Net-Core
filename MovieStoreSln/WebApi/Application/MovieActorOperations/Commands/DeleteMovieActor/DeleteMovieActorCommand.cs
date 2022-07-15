using System.Linq;
using WebApi.DbOperations;
using System;

namespace WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommand
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;


        public DeleteMovieActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public bool Handle(){
            var movieActor = _dbContext.MovieActors.SingleOrDefault(ma => ma.MovieId == MovieId && ma.ActorId == ActorId);
            if(movieActor is null)
                throw new InvalidOperationException("Filmde tanımlı aktör bulunamadı!");
            _dbContext.MovieActors.Remove(movieActor);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);            
        }
    }
}