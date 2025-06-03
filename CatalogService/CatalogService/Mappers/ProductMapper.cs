using System;
using CatalogService.BLL.Dtos;
using CatalogService.Dtos.Product;
using Google.Protobuf;

namespace CatalogService.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this CreateProductRequestDto createProductRequestDto)
    {
        return new ProductDto
        {
            Name = createProductRequestDto.Name,
            Description = createProductRequestDto.Description,
            Price = createProductRequestDto.Price,
        };
    }

    public static ProductImageGrpcCreateRequest ToImageDto(this CreateProductRequestDto createProductRequestDto)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            createProductRequestDto.ImageFile.OpenReadStream().CopyTo(memoryStream);
            return new ProductImageGrpcCreateRequest
            {
                ImageData = ByteString.CopyFrom((memoryStream).ToArray()),
                ImageExtention = Path.GetExtension(createProductRequestDto.ImageFile.FileName)
            };
        }
    }

    public static ProductDto ToDto(this UpdateProductRequestDto updateProductRequestDto)
    {
        return new ProductDto
        {
                Name = updateProductRequestDto.Name,
                Description = updateProductRequestDto.Description,
                Price = updateProductRequestDto.Price
        };
    }

    public static ProductResponseDto ToResponseDto(this ProductDto productDto)
    {
        return new ProductResponseDto
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            OrderAmount = productDto.OrderAmount,
            Rating = productDto.Rating,
            Tags = productDto.Tags.Select(x => x.ToResponseDto()).ToList(),
            ImageId = productDto.ImageId,
        };
    }
}