using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
{
    internal static class ProductOrderMapper
    {
        public static ProductProductOrderDto ToProductProductOrderDto(this ProductOrder model)
        {
            return new ProductProductOrderDto
            {
                ProductId = model.ProductId,
                Amount = model.Amount,
                Order = model.Order.ToDto(),
            };
        }

        public static OrderProductOrderDto ToOrderProductOrderDto(this ProductOrder model)
        {
            return new OrderProductOrderDto
            {
                ProductId = model.ProductId,
                Amount = model.Amount,
                Product = model.Product.ToDto(),
            };
        }

        public static ProductOrder ToModel(this ProductProductOrderDto dto)
        {
            return new ProductOrder
            {
                ProductId = dto.ProductId,
                OrderId = dto.OrderId,
                Amount = dto.Amount,
            };
        }

        public static ProductOrder ToModel(this OrderProductOrderDto dto)
        {
            return new ProductOrder
            {
                ProductId = dto.ProductId,
                OrderId = dto.OrderId,
                Amount = dto.Amount,
            };
        }
    }
}
