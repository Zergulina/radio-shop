using OrderService.BLL.Dtos;
using OrderService.Dtos.OrderUnit;

namespace OrderService.Mappers
{
    public static class OrderUnitMapper
    {
        public static OrderUnitResponseDto ToResponse(this OrderUnitDto dto)
        {
            return new OrderUnitResponseDto
            {
                Amount = dto.Amount,
                ProductId = dto.ProductId,
                Price = dto.Price,
            };
        }
        public static ProductUnitOrderResponseDto ToProductOrderUnitResponse(this OrderUnitDto dto)
        {
            return new ProductUnitOrderResponseDto
            {
                Amount = dto.Amount,
                Product = dto.Product.ToResponse()
            };
        }
        public static OrderUnitDto ToDto(this CreateOrderUnitRequestDto dto)
        {
            return new OrderUnitDto
            {
                Amount = dto.Amount,
                ProductId = dto.ProductId
            };
        }
    }
}
