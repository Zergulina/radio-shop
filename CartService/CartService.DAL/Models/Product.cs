using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public ulong TotalRating { get; set; } = 0;
        public ulong RatingAmount { get; set; } = 0;
        public ulong OrderAmount { get; set; } = 0;
        public string? ImageId { get; set; } = null;
        public List<Tag> Tags { get; set; } = new();
    }
}
