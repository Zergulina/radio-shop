using System.ComponentModel.DataAnnotations;

namespace RadioShop.WEB.Dtos.User
{
    public class UpdateUserRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; private set; }
        public string? MiddleName { get; set; }
        [Required] 
        public string Email { get; set; }
    }
}
