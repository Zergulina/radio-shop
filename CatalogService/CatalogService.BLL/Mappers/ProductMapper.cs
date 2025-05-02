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
                PriceRuble = model.PriceRuble,
                PriceKopek = model.PriceKopek,
                Rating = (byte)(model.TotalRating / model.RatingAmount),
                Tags = model.Tags.Select(x => x.ToDto()).ToList(),
            };
        }

        public static Product ToModel(this ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                PriceRuble = dto.PriceRuble,
                PriceKopek= dto.PriceKopek,
            };
        }
    }