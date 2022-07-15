using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        public int ActorId { get; set; }
        public UpdateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("Güncellemeye çalıştığınız aktör bulunamadı!");
            actor.FirstName = Model.FirstName == default ? actor.FirstName : Model.FirstName;
            actor.LastName = Model.LastName == default ? actor.LastName : Model.LastName;
            actor.IsActive = Model.IsActive == default ? actor.IsActive : Model.IsActive;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}