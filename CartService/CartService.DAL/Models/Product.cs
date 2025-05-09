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
        public long PriceRuble { get; set; } = 0;
        public byte PriceKopek { get; set; } = 0;
        public long TotalRating { get; set; } = 0;
        public long RatingAmount { get; set; } = 0;
        public long OrderAmount { get; set; } = 0;
        public string? ImageId { get; set; } = null;
        public List<Tag> Tags { get; set; } = new();
    }
}
