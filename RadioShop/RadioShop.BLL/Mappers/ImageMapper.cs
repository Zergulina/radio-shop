using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
{
    internal static class ImageMapper
    {
        public static Image ToModel(this ImageDto dto)
        {
            return new Image()
            {
                Id = dto.Id,
                ImageData = dto.ImageData != null ? Convert.FromBase64String(dto.ImageData) : new byte[0],
                ImageExtention = dto.ImageExtention ?? string.Empty
            };
        }

        public static ImageDto ToDto(this Image model)
        {
            return new ImageDto()
            {
                Id = model.Id,
                ImageData = model.ImageData != null ? Convert.ToBase64String(model.ImageData) : null,
                ImageExtention = string.IsNullOrEmpty(model.ImageExtention) ? null : model.ImageExtention
            };
        }
    }
}
