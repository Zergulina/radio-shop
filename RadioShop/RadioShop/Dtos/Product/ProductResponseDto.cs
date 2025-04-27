using RadioShop.BLL.Dtos;
using RadioShop.WEB.Dtos.Image;
using RadioShop.WEB.Dtos.Tag;

namespace RadioShop.WEB.Dtos.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PriceRuble { get; set; }
        public long PriceKopek { get; set; }
        public byte Rating { get; set; }
        public List<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
        public ImageResponseDto? Image { get; set; } = null;
    }
}
