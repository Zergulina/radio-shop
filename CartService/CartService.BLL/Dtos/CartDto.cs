using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Dtos
{
    internal class CartDto
    {
        public string UserId { get; set; }
        public UserDto? User { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public ulong Amount { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
