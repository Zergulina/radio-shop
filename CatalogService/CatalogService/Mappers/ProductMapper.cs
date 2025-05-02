using System;
using CatalogService.BLL.Dtos;
using CatalogService.Dtos.Product;

namespace CatalogService.Mappers;

 public static class ProductMapper
    {
        public static ProductDto ToDto(this CreateProductRequestDto createProductRequestDto)
        {
            return new ProductDto
            {
                Name = createProductRequestDto.Name,
                Description = createProductRequestDto.Description,
                PriceRuble = createProductRequestDto.PriceRuble,
                PriceKopek = createProductRequestDto.PriceKopek,
            };
        }

        public static ProductDto ToDto(this UpdateProductRequestDto updateProductRequestDto)
        {
            return new ProductDto
            {
                Name = updateProductRequestDto.Name,
                Description = updateProductRequestDto.Description,
                PriceRuble = updateProductRequestDto.PriceRuble,
                PriceKopek = updateProductRequestDto.PriceKopek,
            };
        }

        public static ProductResponseDto ToResponseDto(this ProductDto productDto)
        {
            return new ProductResponseDto
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                PriceRuble = productDto.PriceRuble,
                PriceKopek = productDto.PriceKopek,
                Tags = productDto.Tags.Select(x => x.ToResponseDto()).ToList(),
            };
        }
    }