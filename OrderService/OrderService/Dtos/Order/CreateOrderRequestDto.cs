using OrderService.Dtos.OrderUnit;

namespace OrderService.Dtos.Order
{
    public class CreateOrderRequestDto
    {
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CreateOrderUnitRequestDto> Units { get; set; }
    }
}
