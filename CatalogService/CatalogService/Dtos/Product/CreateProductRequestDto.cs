using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Dtos.Product;

public class CreateProductRequestDto
{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long PriceRuble { get; set; }
        [Required]
        public byte PriceKopek { get; set; }
}
