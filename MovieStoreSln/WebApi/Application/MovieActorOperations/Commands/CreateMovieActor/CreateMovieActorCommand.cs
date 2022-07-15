using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using System;
using WebApi.Entities;

namespace WebApi.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommand
    {
        public CreateMovieActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var movie = _dbContext.Movies.Any(m => m.Id == Model.MovieId);
            var actor = _dbContext.Actors.Any(a => a.Id == Model.ActorId);
            var movieActor = _dbContext.MovieActors.SingleOrDefault(ma => ma.MovieId == Model.MovieId && ma.ActorId == Model.ActorId);
            if(!movie)
                throw new InvalidOperationException("Aradığınız film bulunamadı!");
            if(!actor)
                throw new InvalidOperationException("Aktör bulunamadı!");
            if(movieActor is not null)
                throw new InvalidOperationException("Filmde aktör tanımlı!");
            movieActor = _mapper.Map<MovieActor>(Model);
            _dbContext.MovieActors.Add(movieActor);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);            
        }
    }


    public class CreateMovieActorViewModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}