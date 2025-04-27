using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
{
    internal static class ProductRatingMapper
    {
        public static ProductProductRatingDto ToProductProductRatingDto(this ProductRating model) {
            return new ProductProductRatingDto
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                MiddleName = model.User.MiddleName,
                Rating = model.Rating,
                Description = model.Description,
                DateTime = model.DateTime,
            };
        }

        public static UserProductRatingDto ToUserProductRatingDto(this ProductRating model)
        {
            return new UserProductRatingDto
            {
                ProductId = model.ProductId,
                Product = model.Product.ToDto(),
                UserId = model.UserId,
                Rating = model.Rating,
                Description = model.Description,
                DateTime = model.DateTime,
            };
        }

        public static ProductRating ToModel(this ProductProductRatingDto dto)
        {
            return new ProductRating
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Description = dto.Description,
                DateTime = dto.DateTime,
            };
        }

        public static ProductRating ToModel(this UserProductRatingDto dto)
        {
            return new ProductRating
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Description = dto.Description,
                DateTime = dto.DateTime,
            };
        } 
    }
}
