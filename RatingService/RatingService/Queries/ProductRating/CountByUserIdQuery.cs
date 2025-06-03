namespace RatingService.Queries.ProductRating
{
    public class CountByUserIdQuery
    {
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public byte? MinMyRating { get; set; } = null;
        public byte? MaxMyRating { get; set; } = null;
        public byte? MinRating { get; set; } = null;
        public byte? MaxRating { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Tag { get; set; } = null;
        public DateTime? StartCreatedAt { get; set; } = null;
        public DateTime? EndCreatedAt { get; set; } = null;
    }
}
