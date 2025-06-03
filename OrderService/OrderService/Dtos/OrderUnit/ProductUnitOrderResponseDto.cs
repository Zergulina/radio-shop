using OrderService.BLL.Dtos;
using OrderService.Dtos.Product;

namespace OrderService.Dtos.OrderUnit
{
    public class ProductUnitOrderResponseDto
    {
        public ulong Amount { get; set; }
        public ProductResponseDto Product { get; set; }
    }
}
