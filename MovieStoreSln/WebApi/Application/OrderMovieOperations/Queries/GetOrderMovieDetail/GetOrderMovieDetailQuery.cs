using AutoMapper;
using WebApi.DbOperations;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.OrderMovieOperations.Queries.GetOrderMovieDetail
{
    public class GetOrderMovieDetailQuery
    {
        public int OrderId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderMovieDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public OrderMovieDetailViewModel Handle(){
            var orderMovie = _dbContext.OrderMovies.Include(x=>x.Customer).Include(x=>x.Movie).SingleOrDefault(x=>x.Id == OrderId && x.IsVisible == true);
            if(orderMovie is null)
                throw new InvalidOperationException("Satın alma bulunamadı!");

            OrderMovieDetailViewModel returnObj = _mapper.Map<OrderMovieDetailViewModel>(orderMovie);

            return returnObj;
        }
    }

    public class OrderMovieDetailViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal PurchasedPrice { get; set; }
    }
}