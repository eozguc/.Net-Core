using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.MovieActorOperations.Queries.GetActorMovies
{
    public class GetActorMoviesQuery
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ActorMoviesViewModel> Handle()
        {
            var movies = _dbContext.MovieActors.Include(x => x.Movie).ThenInclude(x=>x.Genre).Include(x=>x.Movie.Director).Where(ma => ma.ActorId == ActorId).ToList();
            if (movies is null)
                throw new InvalidOperationException("Aktöre ait filmler bulunamadı!");

            List<ActorMoviesViewModel> actorMovies = _mapper.Map<List<ActorMoviesViewModel>>(movies);
            return actorMovies;
        }

        public class ActorMoviesViewModel
        {
            public int MovieId { get; set; }
            public string Title { get; set; }
            public int ReleaseDate { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
        }
    }
}