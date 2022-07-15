using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieActorOperations.Queries.GetMovieActors
{
    public class GetMovieActorsQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MovieActorsViewModel> Handle(){
            var actors = _dbContext.MovieActors.Include(x=>x.Actor).Where(ma=> ma.MovieId == MovieId).ToList();
            if(actors is null)
                throw new InvalidOperationException("Filme ait aktörler bulunamadı!");
            
            List<MovieActorsViewModel> movieActors = _mapper.Map<List<MovieActorsViewModel>>(actors);
            return movieActors;  
        }

        public class MovieActorsViewModel
        {
            public int ActorId { get; set; }
            public string FullName { get; set; }
            
        }




    }
}