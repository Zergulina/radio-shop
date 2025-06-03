using OrderService.BLL.Dtos;
using OrderService.Dtos.Order;

namespace OrderService.Mappers
{
    public static class OrderMapper
    {
        public static OrderResponseDto ToResponse(this OrderDto dto)
        {
            return new OrderResponseDto
            {
                Id = dto.Id,
                OrderedAt = dto.OrderedAt,
                DeliveryDateTime = dto.DeliveryDateTime,
                DeliveryAddress = dto.DeliveryAddress,
                IsAccepted = dto.IsAccepted,
                Units = dto.Units.Select(x => x.ToResponse()).ToList(),
            };
        }
        public static OrderDto ToDto(this CreateOrderRequestDto dto, string userId)
        {
            return new OrderDto
            {
                DeliveryDateTime = dto.DeliveryDateTime,
                DeliveryAddress = dto.DeliveryAddress,
                Units = dto.Units.Select(x => x.ToDto()).ToList(),
                UserId = userId,
            };
        }
    }
}
