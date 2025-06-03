namespace RatingService.Queries.ProductRating
{
    public class GetAllByProductIdQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public byte? MinRating { get; set; } = null;
        public byte? MaxRating { get; set; } = null;
        public DateTime? StartCreatedAt { get; set; } = null;
        public DateTime? EndCreatedAt { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public string? SortBy { get; set; } = null;
    }
}
