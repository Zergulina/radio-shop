using AuthService;
using CartService.BLL.Dtos;
using CartService.BLL.Exceptions;
using CartService.BLL.Interfaces;
using CartService.BLL.Mappers;
using CartService.DAL.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Services
{
    internal class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserGrpc.UserGrpcClient _userGrpcClient;
        public CartService(ICartRepository cartRepository, UserGrpc.UserGrpcClient _userGrpcClient)
        {
            _cartRepository = cartRepository;
        }
        public async Task<int> CountByProductIdAsync(int productId)
        {
            return await _cartRepository.CountByProductIdAsync(productId);
        }

        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _cartRepository.CountByUserIdAsync(userId);
        }

        public async Task<CartDto?> CreateAsync(CartDto cart)
        {
            var newCart = await _cartRepository.CreateAsync(cart.ToModel());
            if (newCart == null)
            {
                throw new NotFoundException("Product not found");
            }
            var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = cart.UserId });
            return newCart.ToCartDto(user);
        }

        public async Task DeleteAsync(string userId, int productId)
        {
            var wasDeleted = await _cartRepository.DeleteAsync(userId, productId);
            if (!wasDeleted)
            {
                throw new NotFoundException("Cart unit not found");
            }
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _cartRepository.ExistsAsync(userId, productId);
        }

        public async Task<List<CartDto>> GetAllByProductIdAsync(int? pageNumber, int? pageSize, int productId)
        {
            var carts = await _cartRepository.GetAllByProductIdAsync(pageNumber, pageSize, productId);

            if (carts.Count() == 0) return new List<CartDto>();

            var request = new UserGrpcListRequest();
            carts.ForEach(x => request.Ids.Add(x.UserId));
            var users = await _userGrpcClient.GetUserListAsync(request);

            var userDict = users.Users.ToDictionary(x => x.Id);

            return carts.Select(cart =>
            {
                if (userDict.TryGetValue(cart.UserId, out var user))
                {
                    return cart.ToUserCartDto(user);
                }
                return cart.ToUserCartDto();
            }).ToList();
        }

        public async Task<List<CartDto>> GetAllByUserIdAsync(int? pageNumber, int? pageSize, string userId)
        {
            var carts = await _cartRepository.GetAllByUserIdAsync(pageNumber, pageSize, userId);
            return carts.Select(x => x.ToProductCartDto()).ToList();
        }

        public async Task<CartDto?> GetByIdAsync(string userId, int productId)
        {
            var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = userId });
            if (user == null)
            {
                throw new NotFoundException("Cart unit not found");
            }
            var cart = await _cartRepository.GetByIdAsync(userId, productId);
            if (cart == null)
            {
                throw new NotFoundException("Cart unit not found");
            }
            return cart.ToCartDto(user);
        }
    }
}
