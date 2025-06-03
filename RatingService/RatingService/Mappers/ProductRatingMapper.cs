using RatingService.BLL.Dtos;
using RatingService.Dtos.ProductRating;

namespace RatingService.Mappers
{
    public static class ProductRatingMapper
    {
        public static RatingResponseDto ToResponse(this ProductRatingDto dto)
        {
            return new RatingResponseDto
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
            };
        }
        public static ProductRatingReponseDto ToProductRatingResponse(this ProductRatingDto dto)
        {
            return new ProductRatingReponseDto
            {
                UserId = dto.UserId,
                Product = dto.Product!.ToResponse(),
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
            };
        }
        public static UserProductRatingResponseDto ToUserProductRatingResponse(this ProductRatingDto dto)
        {
            return new UserProductRatingResponseDto
            {
                User = dto.User!.ToResponse(),
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
            };
        }
        public static ProductRatingDto ToDto(this CreateProductRatingRequestDto dto, string userId, int productId)
        {
            return new ProductRatingDto
            {
                UserId = userId,
                ProductId = productId,
                Rating = dto.Rating,
                Comment = dto.Comment,
            };
        }
        public static ProductRatingDto ToDto(this UpdateProductRatingRequestDto dto)
        {
            return new ProductRatingDto
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
            };
        }
    }
}
