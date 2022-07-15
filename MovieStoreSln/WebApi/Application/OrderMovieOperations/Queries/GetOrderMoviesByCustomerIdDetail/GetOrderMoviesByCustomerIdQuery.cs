using AutoMapper;
using WebApi.DbOperations;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Application.OrderMovieOperations.Queries.GetOrderMoviesByCustomerIdDetail
{
    public class GetOrderMoviesByCutomerIdQuery
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderMoviesByCutomerIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderMoviesByCustomerIdViewModel> Handle()
        {
            var orderMovie = _dbContext.OrderMovies.Include(x => x.Movie).Where(x => x.CustomerId == CustomerId);
            if (orderMovie is null)
                throw new InvalidOperationException("Satın almalar bulunamadı!");

            List<OrderMoviesByCustomerIdViewModel> returnObj = _mapper.Map<List<OrderMoviesByCustomerIdViewModel>>(orderMovie);

            return returnObj;
        }
    }

    public class OrderMoviesByCustomerIdViewModel
    {
        public int Id { get; set; }
        public string Movie { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal PurchasedPrice { get; set; }
        public bool IsVisible { get; set; }
    }
}