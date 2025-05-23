using System;

namespace CatalogService.BLL.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public byte Rating { get; set; } = 0;
    public long OrderAmount { get; set; } = 0;
    public List<TagDto> Tags { get; set; } = new List<TagDto>();
    public string? ImageId { get; set; } = null;
}
