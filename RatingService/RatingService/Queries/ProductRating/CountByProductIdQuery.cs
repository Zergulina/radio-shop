namespace RatingService.Queries.ProductRating
{
    public class CountByProductIdQuery
    {
        public byte? MinRating { get; set; } = null;
        public byte? MaxRating { get; set; } = null;
        public DateTime? StartCreatedAt { get; set; } = null;
        public DateTime? EndCreatedAt { get; set; } = null;
    }
}
