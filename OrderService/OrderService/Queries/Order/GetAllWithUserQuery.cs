namespace OrderService.Queries.Order
{
    public class GetAllWithUserQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? UserId { get; set; } = null;
        public DateTime? StartOrderedAt { get; set; } = null;
        public DateTime? EndOrderedAt { get; set; } = null;
        public DateTime? StartDeliveryDateTime { get; set; } = null;
        public DateTime? EndDeliveryDateTime { get; set; } = null;
        public ulong? MinUnitsAmount { get; set; } = null;
        public ulong? MaxUnitsAmount { get; set; } = null;
        public decimal? MinCost { get; set; } = null;
        public decimal? MaxCost { get; set; } = null;
        public bool? IsAccepted { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public string? SortBy { get; set; } = null;
    }
}
