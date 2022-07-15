using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using Webapi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefresTokenExpireDate > DateTime.Now);
            if (customer is null)
                throw new InvalidOperationException("Valid bir refresh token bulunamadı!");

            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(customer);

            customer.RefreshToken = token.RefreshToken;
            customer.RefresTokenExpireDate = token.Expiration.AddMinutes(5);
            _dbContext.SaveChanges();

            return token;
        }
    }
}