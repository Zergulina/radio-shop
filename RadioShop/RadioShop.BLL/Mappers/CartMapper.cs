using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Mappers
{
    internal static class CartMapper
    {
        public static ProductCartDto ToProductCartDto( this Cart model)
        {
            return new ProductCartDto
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                MiddleName = model.User.MiddleName,
                Amount = model.Amount,
                DateTime = model.DateTime,
            };
        }

        public static UserCartDto ToUserCartDto(this Cart model)
        {
            return new UserCartDto
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                Amount = model.Amount,
                DateTime = model.DateTime,
                Product = model.Product.ToDto(),
            };
        }

        public static Cart ToModel(this ProductCartDto dto)
        {
            return new Cart
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                DateTime = dto.DateTime,
            };
        }

        public static Cart ToModel(this UserCartDto dto)
        {
            return new Cart
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                DateTime = dto.DateTime,
            };
        }
    }
}
