using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var actor = _dbContext.Actors.SingleOrDefault(a => a.FirstName == Model.FirstName && a.LastName == Model.LastName);
            if(actor is not null)
                throw new InvalidOperationException("Eklemeye çalıştığınız aktör zaten mevcut!");
            actor = _mapper.Map<Actor>(Model);  // → AutoMapper
            _dbContext.Actors.Add(actor);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class CreateActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}