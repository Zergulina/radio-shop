using OrderService.BLL.Dtos;
using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Mappers
{
    public static class OrderUnitMapper
    {
        public static OrderUnitDto ToDto(this OrderUnit model)
        {
            return new OrderUnitDto
            {
                ProductId = model.ProductId,
                OrderId = model.OrderId,
                Amount = model.Amount,
                Price = model.Product.Price
            };
        }

        public static OrderUnitDto ToProductDto(this OrderUnit model)
        {
            return new OrderUnitDto
            {
                ProductId = model.ProductId,
                OrderId = model.OrderId,
                Amount = model.Amount,
                Price = model.Product.Price,
                Product = model.Product.ToDto()
            };
        }

        public static OrderUnit ToModel(this OrderUnitDto dto)
        {
            return new OrderUnit
            {
                ProductId = dto.ProductId,
                OrderId = dto.OrderId,
                Amount = dto.Amount,
            };
        }
    }
}
