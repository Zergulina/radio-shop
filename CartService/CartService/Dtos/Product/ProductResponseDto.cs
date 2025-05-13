using CartService.Dtos.Tag;

namespace CartService.Dtos.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public byte Rating { get; set; } = 0;
        public long OrderAmount { get; set; } = 0;
        public List<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
        public string? ImageId { get; set; } = null;
    }
}
