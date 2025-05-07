using ImageService.BLL.Dtos;
using ImageService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.BLL.Mappers
{
    internal static class ImageMapper
    {
        public static ImageDto ToDto(this Image model)
        {
            return new ImageDto {
                Id = model.Id,
                ImageData = model.ImageData,
                ImageType = model.ImageType,
            };
        }

        public static Image ToModel (this ImageDto dto)
        {
            return new Image
            {
                Id = dto.Id,
                ImageData = dto.ImageData,
                ImageType = dto.ImageType,
            };
        }
    }
}
