using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.DAL.Models
{
    public class ProductRating
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public byte Rating {  get; set; }
        public string Comment { get; set; } = string.Empty;
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
