using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
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
                Rating = model.ProductRatings.Count() > 0 ? (byte)model.ProductRatings.Average(x => x.Rating) : (byte)0,
                Tags = model.Tags.Select(x => x.ToDto()).ToList(),
                Image = model.Image?.ToDto()
            };
        }

        public static Product ToModel(this ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                PriceRuble = dto.PriceRuble,
                PriceKopek= dto.PriceKopek,
            };
        }

        public static Image ToImageFromDto(this ProductDto dto)
        {
            return new Image
            {
                Id = dto.ImageId ?? 0,
                ImageData = dto?.Image?.ImageData != null ? Convert.FromBase64String(dto.Image.ImageData) : new byte[0],
                ImageExtention = dto?.Image?.ImageExtention ?? string.Empty
            };
        }
    }
}
