using OrderService.BLL.Dtos;
using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Mappers
{
    internal static class OrderMapper
    {
        public static OrderDto ToDto(this Order model)
        {
            return new OrderDto
            {
                Id = model.Id,
                UserId = model.UserId,
                OrderedAt = model.OrderedAt,
                DeliveryDateTime = model.DeliveryDateTime,
                DeliveryAddress = model.DeliveryAddress,
                IsAccepted = model.IsAccepted,
                Units = model.Units.Select(x => x.ToDto()).ToList(),
            };
        }

        public static OrderDto ToDto(this Order model, UserGrpcResponse user)
        {
            return new OrderDto
            {
                Id = model.Id,
                UserId = model.UserId,
                OrderedAt = model.OrderedAt,
                DeliveryDateTime = model.DeliveryDateTime,
                DeliveryAddress = model.DeliveryAddress,
                IsAccepted = model.IsAccepted,
                Units = model.Units.Select(x => x.ToDto()).ToList(),
                User = user.ToDto(),
            };
        }

        public static Order ToModel(this OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                UserId = dto.UserId,
                OrderedAt = dto.OrderedAt,
                DeliveryDateTime = dto.DeliveryDateTime,
                DeliveryAddress = dto.DeliveryAddress,
                IsAccepted = dto.IsAccepted,
                Units = dto.Units.Select(x => x.ToModel()).ToList(),
            };
        }
    }
}
