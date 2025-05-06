using CartService.Dtos.Product;

namespace CartService.Dtos.Cart
{
    public class ProductCartResponseDto
    {
        public ulong Amount { get; set; }
        public DateTime AddedAt { get; set; }
        public ProductResponseDto Product { get; set; }
    }
}
