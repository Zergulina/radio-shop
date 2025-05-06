using CartService.Dtos.Product;
using CartService.Dtos.User;

namespace CartService.Dtos.Cart
{
    public class CartResponseDto
    {
        public UserResponseDto User { get; set; }
        public ProductResponseDto Product { get; set; }
        public ulong Amount { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
 