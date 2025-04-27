using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PriceRuble { get; set; }
        public long PriceKopek { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
        public List<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public int? ImageId { get; set; } = null;
        public Image? Image { get; set; } = null;
    }
}
