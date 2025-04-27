using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Tag
{
    public class UpdateTagRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
