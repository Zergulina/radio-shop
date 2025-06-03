using RatingService.Dtos.Product;
using RatingService.Dtos.User;

namespace RatingService.Dtos.ProductRating
{
    public class UserProductRatingResponseDto
    {
        public UserResponseDto User { get; set; }
        public int ProductId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
