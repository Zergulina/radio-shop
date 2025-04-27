using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Image
{
    public class CreateImageRequestDto
    {
        [Required]
        public string ImageData { get; set; }
        [Required]
        public string ImageExtention { get; set; }
    }
}
