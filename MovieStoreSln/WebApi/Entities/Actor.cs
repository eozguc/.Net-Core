
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Actor : Person
    {
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}