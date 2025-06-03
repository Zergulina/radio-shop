namespace OrderService.Queries.OrderUnit
{
    public class CountQuery
    {
        public ulong? MinAmount { get; set; } = null;
        public ulong? MaxAmount { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public decimal? MinCost { get; set; } = null;
        public decimal? MaxCost { get; set; } = null;
        public byte? MinRating { get; set; } = null;
        public byte? MaxRating { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Tag { get; set; } = null;
    }
}
