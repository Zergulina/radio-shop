using OrderService.BLL.Dtos;
using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Mappers
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
