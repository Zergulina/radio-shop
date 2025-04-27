using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.Cart
{
    public class UpdateCartRequestDto
    {
        [Required]
        public ulong Amount { get; set; }
    }
}
