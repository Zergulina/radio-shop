using System;
using CatalogService.Dtos.Tag;

namespace CatalogService.Dtos.Product;

public class ProductResponseDto
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public byte Rating { get; set; }
        public List<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
        public string? ImageId {  get; set; }
}
