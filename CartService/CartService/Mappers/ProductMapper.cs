using CartService.BLL.Dtos;
using CartService.Dtos.Product;

namespace CartService.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponseDto ToResponse(this ProductDto dto)
        {
            return new ProductResponseDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                PriceRuble = dto.PriceRuble,
                PriceKopek = dto.PriceKopek,
                Rating = dto.Rating,
                OrderAmount = dto.OrderAmount,
                Tags = dto.Tags.Select(x => x.ToResponse()).ToList(),
                ImageId = dto.ImageId,
            };
        }
    }
}
