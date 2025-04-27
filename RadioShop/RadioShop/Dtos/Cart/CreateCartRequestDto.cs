using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Cart
{
    public class CreateCartRequestDto
    {
        [Required]
        public ulong Amount { get; set; }
    }
}
