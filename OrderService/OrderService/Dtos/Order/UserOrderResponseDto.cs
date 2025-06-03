using OrderService.BLL.Dtos;
using OrderService.Dtos.OrderUnit;
using OrderService.Dtos.User;

namespace OrderService.Dtos.Order
{
    public class UserOrderResponseDto
    {
        public int Id { get; set; }
        public DateTime OrderedAt { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsAccepted { get; set; }
        public List<OrderUnitResponseDto> Units { get; set; }
        public UserResponseDto User { get; set; }
    }
}
