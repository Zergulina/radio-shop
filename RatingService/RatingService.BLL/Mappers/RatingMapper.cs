using RatingService.BLL.Dtos;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Mappers
{
    internal static class RatingMapper
    {
        public static ProductRating ToModel(this ProductRatingDto dto)
        {
            return new ProductRating
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
            };
        }
        public static ProductRatingDto ToProductRatingDto(this ProductRating model)
        {
            return new ProductRatingDto
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Product = model.Product.ToDto(),
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = model.CreatedAt,
            };
        }

        public static ProductRatingDto ToRatingDto(this ProductRating model)
        {
            return new ProductRatingDto
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = model.CreatedAt,
            };
        }

        public static ProductRatingDto ToUserRatingDto(this ProductRating model, UserGrpcResponse user)
        {
            return new ProductRatingDto
            {
                UserId = model.UserId,
                User = user.ToDto(),
                ProductId = model.ProductId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = model.CreatedAt,
            };
        }
    }
}
