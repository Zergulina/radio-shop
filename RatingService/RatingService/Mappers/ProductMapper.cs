using RatingService.BLL.Dtos;
using RatingService.Dtos.Product;

namespace RatingService.Mappers
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
                Price = dto.Price,
                Rating = dto.Rating,
                OrderAmount = dto.OrderAmount,
                Tags = dto.Tags.Select(x => x.ToResponse()).ToList(),
                ImageId = dto.ImageId,
            };
        }
    }
}
