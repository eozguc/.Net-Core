using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
using System;

namespace WebApi.Application.OrderMovieOperations.Commands.CreateOrderMovie
{
    public class CreateOrderMovieCommand
    {
        public CreateOrderMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateOrderMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var orderMovie = _mapper.Map<OrderMovie>(Model);
            orderMovie.PurchasedDate = DateTime.Now;
            _dbContext.OrderMovies.Add(orderMovie);
            int result = _dbContext.SaveChanges();

            return Convert.ToBoolean(result);
        }
    }

    public class CreateOrderMovieModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal PurchasedPrice { get; set; }
    }
}