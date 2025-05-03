using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL.Models
{
    public class Cart
    { 
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ulong Amount { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
