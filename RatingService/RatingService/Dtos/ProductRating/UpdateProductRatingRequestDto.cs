namespace RatingService.Dtos.ProductRating
{
    public class UpdateProductRatingRequestDto
    {
        public byte Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
