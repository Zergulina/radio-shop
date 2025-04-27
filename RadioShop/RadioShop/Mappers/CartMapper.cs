using RadioShop.BLL.Dtos;
using RadioShop.WEB.Dtos.Cart;

namespace RadioShop.WEB.Mappers
{
    public static class CartMapper
    {
        public static ProductCartDto ToProductCartDto(this CreateCartRequestDto createRequestDto)
        {
            return new ProductCartDto
            {
                Amount = createRequestDto.Amount
            };
        }

        public static ProductCartDto ToProductCartDto(this UpdateCartRequestDto updateRequestDto)
        {
            return new ProductCartDto
            {
                Amount = updateRequestDto.Amount
            };
        }

        public static UserCartResponseUnitDto ToUserCartResponseUnitDto(this UserCartDto userCartDto)
        {
            return new UserCartResponseUnitDto
            {
                Amount = userCartDto.Amount,
                Product = userCartDto.Product.ToResponseDto()
            };
        }

        public static UserCartResponseDto? ToUserCartResponseDto(this List<UserCartDto> userCartDtos)
        {
            return userCartDtos.Count() > 0 ? new UserCartResponseDto
            {
                UserId = userCartDtos[0].UserId,
                CartUnits = userCartDtos.Select(x => x.ToUserCartResponseUnitDto()).ToList(),
            } : null;
        }
    }
}
