namespace RatingService.Queries.ProductRating
{
    public class GetAllByUserIdQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
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
        public bool IsDescending { get; set; } = false;
        public string? SortBy { get; set; } = null;
    }
}
