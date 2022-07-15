using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
using System;
using System.Linq;

namespace WebApi.Application.OrderMovieOperations.Commands.UpdateOrderMovie
{
    public class UpdateOrderMovieCommand
    {
        public int OrderId { get; set; }
        public UpdateOrderMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateOrderMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var orderMovie = _dbContext.OrderMovies.SingleOrDefault(om => om.Id == OrderId);
            if (orderMovie != null)
                orderMovie.IsVisible = Model.IsVisible != default ? orderMovie.IsVisible : Model.IsVisible;
            int result = _dbContext.SaveChanges();

            return Convert.ToBoolean(result);
        }
    }

    public class UpdateOrderMovieModel
    {
        public bool IsVisible { get; set; }
    }
}