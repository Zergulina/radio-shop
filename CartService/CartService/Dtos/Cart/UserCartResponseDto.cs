using CartService.Dtos.User;

namespace CartService.Dtos.Cart
{
    public class UserCartResponseDto
    {
        public ulong Amount { get; set; }
        public DateTime AddedAt { get; set; }
        public UserResponseDto? User {  get; set; }
    }
}
