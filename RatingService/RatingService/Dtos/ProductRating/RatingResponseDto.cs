using RatingService.Dtos.Product;

namespace RatingService.Dtos.ProductRating
{
    public class RatingResponseDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
