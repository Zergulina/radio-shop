namespace RadioShop.WEB.Queries.Cart
{
    public class CountByUserIdQuery
    {
        public ulong? MinAmount { get; set; } = null;
        public ulong? MaxAmount { get; set; } = null;
        public DateTime? StartDateTime { get; set; } = null;
        public DateTime? EndDateTime { get; set; } = null;
        public ulong? MinPrice { get; set; } = null;
        public ulong? MaxPrice { get; set; } = null;
        public string? ProductName { get; set; } = null;
    }
}
