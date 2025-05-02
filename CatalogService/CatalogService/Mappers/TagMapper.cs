using System;
using CatalogService.BLL.Dtos;
using CatalogService.Dtos.Tag;

namespace CatalogService.Mappers;

public static class TagMapper
{
    public static TagDto ToDto(this CreateTagRequestDto createTagRequestDto)
    {
        return new TagDto
        {
            Name = createTagRequestDto.Name
        };
    }

    public static TagDto ToDto(this UpdateTagRequestDto updateTagRequestDto)
    {
        return new TagDto
        {
            Name = updateTagRequestDto.Name,
        };
    }

    public static TagResponseDto ToResponseDto(this TagDto tagDto)
    {
        return new TagResponseDto
        {
            Id = tagDto.Id,
            Name = tagDto.Name,
        };
    }
}