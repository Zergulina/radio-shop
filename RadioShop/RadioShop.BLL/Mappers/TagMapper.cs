using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
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

        public static Tag ToModel(this TagDto dto)
        {
            return new Tag
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
