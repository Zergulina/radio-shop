using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Dtos
{
    public class OrderUnitDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public ulong Amount { get; set; }
        public decimal Price { get; set; }
        public ProductDto Product { get; set; }
    }
}
