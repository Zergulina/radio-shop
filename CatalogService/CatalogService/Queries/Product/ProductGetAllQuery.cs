using System;

namespace CatalogService.Queries.Product;

public class ProductGetAllQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public ulong? MinPrice { get; set; } = null;
    public ulong? MaxPrice { get; set; } = null;
    public byte? MinRating { get; set; } = null;
    public byte? MaxRating { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? Tag { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public string? SortBy { get; set; } = null;
}