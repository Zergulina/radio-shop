using System;

namespace CatalogService.Queries.Product;

public class ProductCountQuery
{
    public decimal? MinPrice { get; set; } = null;
    public decimal? MaxPrice { get; set; } = null;
    public byte? MinRating { get; set; } = null;
    public byte? MaxRating { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? Tag { get; set; } = null;
}
