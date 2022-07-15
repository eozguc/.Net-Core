using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomerDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CustomerDetailViewModel Handle()
        {
            var customer = _dbContext.Customers.Include(x => x.CustomerFavoritGenres)
                                            .ThenInclude(cfg => cfg.Genre)
                                            .Include(x => x.OrderMovies)
                                            .ThenInclude(x => x.Movie).SingleOrDefault(c => c.Id == CustomerId);
            if (customer is null)
                throw new InvalidOperationException("Aradığınız kullanıcı bulunamadı!");
            CustomerDetailViewModel returnObj = _mapper.Map<CustomerDetailViewModel>(customer);
            return returnObj;
        }
    }

    public class CustomerDetailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<OrderMovieVm> OrderMovies { get; set; }
        public List<CustomerFavoritGenreVm> FavoritGenres { get; set; }

        public struct OrderMovieVm
        {
            public int Id { get; set; }
            public MovieDetailVm Movie { get; set; }
            public decimal PurchasedPrice { get; set; }
            public DateTime PurchasedDate { get; set; }
            public bool IsVisible { get; set; }
        }

        public struct CustomerFavoritGenreVm
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public struct MovieDetailVm
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
        }

    }
}