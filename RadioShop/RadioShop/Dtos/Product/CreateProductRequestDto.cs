using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Product
{
    public class CreateProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long PriceRuble { get; set; }
        [Required]
        public long PriceKopek { get; set; }
        public string? ImageData { get; set; } = null;
        public string? ImageExtention { get; set; } = null;
    }
}
