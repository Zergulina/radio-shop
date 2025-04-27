using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Dtos
{
    public class OrderProductOrderDto
    {
        public int OrderId { get; set; }
        public ulong Amount { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
    }
}
