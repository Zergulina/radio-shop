using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Dtos.Product;

public class TagRequestDto
{
    [Required]
    public int[] TagIds { get; set; }
}
