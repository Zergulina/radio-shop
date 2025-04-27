using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long PriceRuble { get; set; }
        [Required]
        public long PriceKopek { get; set; }
    }
}
