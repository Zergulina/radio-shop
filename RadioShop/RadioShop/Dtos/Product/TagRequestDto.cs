using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Product
{
    public class TagRequestDto
    {
        [Required]
        public int[] TagIds { get; set; }  
    }
}
