
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActorDetailViewModel Handle()
        {
            var actor = _dbContext.Actors.Include(a => a.MovieActors).ThenInclude(ma => ma.Movie).SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("Aradığınız aktör bulunamadı!");

            ActorDetailViewModel returnObj = _mapper.Map<ActorDetailViewModel>(actor);
            return returnObj;
        }
    }

    public class ActorDetailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<ActorMoviesVm> Movies { get; set; }

        public struct ActorMoviesVm
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}