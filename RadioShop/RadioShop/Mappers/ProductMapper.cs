using RadioShop.BLL.Dtos;
using RadioShop.WEB.Dtos.Product;

namespace RadioShop.WEB.Mappers
{
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
                Image = createProductRequestDto.ImageData != null && createProductRequestDto.ImageExtention != null ? new ImageDto
                {
                    ImageData = createProductRequestDto.ImageData,
                    ImageExtention = createProductRequestDto.ImageExtention,
                } : null
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
                Image = productDto.Image?.ToResponseDto(),
                Tags = productDto.Tags.Select(x => x.ToResponseDto()).ToList(),
            };
        }
    }
}
