using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
{
    internal static class OrderMapper
    {
        public static OrderDto ToDto(this Order model)
        {
            return new OrderDto
            {
                Id = model.Id,
                UserId = model.UserId,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                MiddleName = model.User.MiddleName,
                OrderDateTime = model.OrderDateTime,
                DeliveryLocation = model.DeliveryLocation,
                DeliveryDateTime = model.DeliveryDateTime,
            };
        }

        public static Order ToModel(this OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                UserId = dto.UserId,
                OrderDateTime = dto.OrderDateTime,
                DeliveryLocation = dto.DeliveryLocation,
                DeliveryDateTime = dto.DeliveryDateTime,
            };
        }
    }
}
