using RatingService.BLL.Dtos;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Mappers
{
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
                Rating = model.RatingAmount > 0 ? model.TotalRating / model.RatingAmount : 0,
                OrderAmount = model.OrderAmount,
                Tags = model.Tags.Select(x => x.ToDto()).ToList(),
                ImageId = model.ImageId,
            };
        }
    }
}
