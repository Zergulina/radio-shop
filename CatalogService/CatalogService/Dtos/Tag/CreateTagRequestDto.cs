using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Dtos.Tag;

public class CreateTagRequestDto
{
    [Required]
    public string Name { get; set; }
}
