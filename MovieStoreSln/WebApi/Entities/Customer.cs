using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Customer : Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefresTokenExpireDate { get; set; }
        public ICollection<OrderMovie> OrderMovies { get; set; }
        public ICollection<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }
    }
}