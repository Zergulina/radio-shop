namespace RadioShop.WEB.Queries.Product
{
    public class ProductCountQuery
    {
        public ulong? MinPrice { get; set; } = null;
        public ulong? MaxPrice { get; set; } = null;
        public byte? MinRating { get; set; } = null;
        public byte? MaxRating { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Tag { get; set; } = null;
    }
}
