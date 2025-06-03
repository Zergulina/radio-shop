using OrderService.BLL.Dtos;

namespace OrderService.Dtos.OrderUnit
{
    public class OrderUnitResponseDto
    {
        public int ProductId { get; set; }
        public ulong Amount { get; set; }
        public decimal Price { get; set; }
    }
}
