using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Tag
{
    public class CreateTagRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
