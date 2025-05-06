using CartService.Dtos.Tag;

namespace CartService.Dtos.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long PriceRuble { get; set; }
        public byte PriceKopek { get; set; }
        public byte Rating { get; set; } = 0;
        public long OrderAmount { get; set; } = 0;
        public List<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
        public int? ImageId { get; set; } = null;
    }
}
