namespace RadioShop.WEB.Queries.Cart
{
    public class GetAllByUserIdQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public ulong? MinAmount { get; set; } = null;
        public ulong? MaxAmount { get; set; } = null;
        public DateTime? StartDateTime { get; set; } = null;
        public DateTime? EndDateTime { get; set; } = null;
        public ulong? MinPrice { get; set; } = null;
        public ulong? MaxPrice { get; set; } = null;
        public string? ProductName { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public string? SortBy { get; set; } = null;
    }
}
