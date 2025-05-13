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
        public decimal Price { get; set; }
        public IFormFile? ImageFile { get; set; }
}
