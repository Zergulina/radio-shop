using RatingService.BLL.Dtos;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Mappers
{
    internal static class TagMapper
    {
        public static TagDto ToDto(this Tag model)
        {
            return new TagDto
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
