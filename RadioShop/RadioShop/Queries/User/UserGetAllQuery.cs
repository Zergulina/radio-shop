namespace RadioShop.WEB.Queries.User
{
    public class UserGetAllQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Name { get; set; } = null;
        public string SortBy { get; set; } = string.Empty;
        public bool IsDescending { get; set; } = false;
    }
}
