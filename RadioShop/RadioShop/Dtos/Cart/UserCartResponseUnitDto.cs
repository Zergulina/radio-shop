using RadioShop.WEB.Dtos.Product;

namespace RadioShop.WEB.Dtos.Cart
{
    public class UserCartResponseUnitDto
    {
        public ulong Amount { get; set; }
        public ProductResponseDto Product { get; set; }
    }
}
