using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMovieDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public MovieDetailViewModel Handle(){
            var movie = _dbContext.Movies.Include(m=>m.Genre).Include(m=>m.Director).Include(m=>m.MovieActors).ThenInclude(ma=>ma.Actor).SingleOrDefault(x=>x.Id==MovieId);
            if (movie is null)
                throw new InvalidOperationException("Aradığınız film bulunamadı!");
                
            MovieDetailViewModel returnObj = _mapper.Map<MovieDetailViewModel>(movie);
            return returnObj;
        }
    }

    public class MovieDetailViewModel
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