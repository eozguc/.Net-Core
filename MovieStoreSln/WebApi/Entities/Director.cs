
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Director : Person
    {
        public ICollection<Movie> Movies { get; set; }
    }
}