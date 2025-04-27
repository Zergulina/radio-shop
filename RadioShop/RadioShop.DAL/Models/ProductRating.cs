using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Models
{
    public class ProductRating
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public byte Rating { get; set; }
        public string? Description {  get; set; }
        public DateTime DateTime { get; set; }
    }
}
