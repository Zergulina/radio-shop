using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Dtos.Tag;

public class UpdateTagRequestDto
{
    [Required]
    public string Name { get; set; }
}
