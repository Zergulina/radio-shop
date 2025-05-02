using System;
using CatalogService.Dtos.Tag;

namespace CatalogService.Dtos.Product;

public class ProductResponseDto
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PriceRuble { get; set; }
        public byte PriceKopek { get; set; }
        public byte Rating { get; set; }
        public List<TagResponseDto> Tags { get; set; } = new List<TagResponseDto>();
}
