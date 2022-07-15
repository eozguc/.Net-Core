using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MoviesViewModel> Handle(){
            var movies = _dbContext.Movies.Include(m=>m.Genre).Include(m=>m.Director).Include(m=>m.MovieActors).ThenInclude(ma=>ma.Actor).OrderBy(x=>x.Id);
            List<MoviesViewModel> returnObj = _mapper.Map<List<MoviesViewModel>>(movies);
            return returnObj;
        }
    }

    public class MoviesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public decimal Price { get; set; }  
        public List<MovieActorVm> Actors { get; set; }

        public struct MovieActorVm
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
    }
}