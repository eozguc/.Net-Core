using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class OrderMovie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal PurchasedPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
        public bool IsVisible { get; set; } = true;
    }
}