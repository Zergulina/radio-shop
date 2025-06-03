namespace RatingService.Dtos.ProductRating
{
    public class CreateProductRatingRequestDto
    {
        public byte Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
