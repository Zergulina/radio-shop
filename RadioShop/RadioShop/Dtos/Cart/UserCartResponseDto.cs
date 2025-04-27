using RadioShop.WEB.Dtos.Product;

namespace RadioShop.WEB.Dtos.Cart
{
    public class UserCartResponseDto
    {
        public string UserId { get; set; }
        public List<UserCartResponseUnitDto> CartUnits { get; set; }
    }
}
