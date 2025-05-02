using System;
using CatalogService.BLL.Dtos;
using CatalogService.DAL.Models;

namespace CatalogService.BLL.Mappers;

internal static class TagMapper
    {
        public static TagDto ToDto(this Tag model)
        {
            return new TagDto
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public static Tag ToModel(this TagDto dto)
        {
            return new Tag
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }