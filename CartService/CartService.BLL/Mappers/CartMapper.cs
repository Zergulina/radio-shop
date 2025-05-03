using AuthService;
using CartService.BLL.Dtos;
using CartService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Mappers
{
    internal static class CartMapper
    {
        public static Cart ToModel(this CartDto dto)
        {
            return new Cart
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                AddedAt = dto.AddedAt,
                Amount = dto.Amount,
            };
        }
        public static CartDto ToProductCartDto(this Cart model)
        {
            return new CartDto
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                AddedAt = model.AddedAt,
                Amount = model.Amount,
                Product = model.Product.ToDto(),
            };
        }

        public static CartDto ToUserCartDto(this Cart model, UserGrpcResponse? user = null)
        {
            return new CartDto
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                AddedAt = model.AddedAt,
                Amount = model.Amount,
                User = user?.ToDto(),
            };
        }

        public static CartDto ToCartDto(this Cart model, UserGrpcResponse user)
        {
            return new CartDto()
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                AddedAt = model.AddedAt,
                Amount = model.Amount,
                User = user.ToDto(),
                Product = model.Product.ToDto()
            };
        }
    }
}
