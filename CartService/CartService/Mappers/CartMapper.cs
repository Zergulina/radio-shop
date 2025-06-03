using CartService.BLL.Dtos;
using CartService.Dtos.Cart;

namespace CartService.Mappers
{
    public static class CartMapper
    {
        public static CartResponseDto ToResponse(this CartDto dto)
        {
            return new CartResponseDto
            {
                User = dto.User.ToResponse(),
                Product = dto.Product.ToResponse(),
                AddedAt = dto.AddedAt,
                Amount = dto.Amount,
            };
        }

        public static ProductCartResponseDto ToProductCartResponse(this CartDto dto)
        {
            return new ProductCartResponseDto
            {
                Product = dto.Product.ToResponse(),
                AddedAt = dto.AddedAt,
                Amount = dto.Amount,
            };
        }

        public static UserCartResponseDto ToUserCartResponse(this CartDto dto)
        {
            return new UserCartResponseDto
            {
                User = dto.User?.ToResponse(),
                AddedAt = dto.AddedAt,
                Amount = dto.Amount,
            };
        }

        public static CartDto ToDto(this CreateCartRequestDto dto, string userId, int productId)
        {
            return new CartDto
            {
                UserId = userId,
                ProductId = productId,
                Amount = dto.Amount
            };
        }

        public static CartDto ToDto(this UpdateCartRequestDto dto)
        {
            return new CartDto
            {
                Amount = dto.Amount
            };
        }
    }
}
