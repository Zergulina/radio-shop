using CartService.BLL.Dtos;
using CartService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Mappers
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
                PriceRuble = model.PriceRuble,
                PriceKopek = model.PriceKopek,
                Rating = (byte)(model.TotalRating / model.RatingAmount),
                OrderAmount = model.OrderAmount,
                Tags = model.Tags.Select(x => x.ToDto()).ToList(),
                ImageId = model.ImageId,
            };
        }
    }
}
