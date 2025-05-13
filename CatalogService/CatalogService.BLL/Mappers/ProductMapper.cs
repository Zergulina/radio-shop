using System;
using CatalogService.BLL.Dtos;
using CatalogService.DAL.Models;

namespace CatalogService.BLL.Mappers;

internal static class ProductMapper
    {
        public static ProductDto ToDto(this Product model)
        {
            return new ProductDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Rating = (byte)(model.RatingAmount > 0 ? model.TotalRating / model.RatingAmount : 0),
                Tags = model.Tags.Select(x => x.ToDto()).ToList(),
                ImageId = model.ImageId,
            };
        }

        public static Product ToModel(this ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageId = dto.ImageId,
            };
        }
    }