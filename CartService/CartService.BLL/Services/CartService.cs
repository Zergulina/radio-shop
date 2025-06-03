using CartService.BLL.Dtos;
using CartService.BLL.Exceptions;
using CartService.BLL.Interfaces;
using CartService.BLL.Mappers;
using CartService.DAL.Interfaces;
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
        private readonly IProductRepository _productRepository;
        private readonly UserGrpc.UserGrpcClient _userGrpcClient;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository, UserGrpc.UserGrpcClient userGrpcClient)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userGrpcClient = userGrpcClient;
        }
        public async Task<int> CountByProductIdAsync(int productId)
        {
            var exists = await _productRepository.ExistsAsync(productId);
            if (!exists)
            {
                throw new NotFoundException("Product not found");
            }
            return await _cartRepository.CountByProductIdAsync(productId);
        }

        public async Task<int> CountByUserIdAsync(string userId)
        {
            var response = await _userGrpcClient.UserExistsAsync(new UserGrpcRequest { Id = userId });
            if (!response.Exists)
            {
                throw new NotFoundException("User not found");
            }
            return await _cartRepository.CountByUserIdAsync(userId);
        }

        public async Task<CartDto> CreateAsync(CartDto cart)
        {   
            cart.AddedAt = DateTime.UtcNow;
            try
            {
                var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = cart.UserId } );
                if (string.IsNullOrEmpty(user.Id))
                {
                    throw new NotFoundException("User not found");
                }
                var productExists = await _productRepository.ExistsAsync(cart.ProductId);
                if (!productExists)
                {
                    throw new NotFoundException("Product not found");
                }
                var exists = await _cartRepository.ExistsAsync(cart.UserId, cart.ProductId);
                if (exists)
                {
                    throw new AlreadyExistsException("Cart unit already exists");
                }
                var newCart = await _cartRepository.CreateAsync(cart.ToModel());
                return newCart.ToProductCartDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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
            var exists = await _productRepository.ExistsAsync(productId);
            if (!exists)
            {
                throw new NotFoundException("Product not found");
            }

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
            var response = await _userGrpcClient.UserExistsAsync(new UserGrpcRequest { Id = userId });
            if (!response.Exists)
            {
                throw new NotFoundException("User not found");
            }
            var carts = await _cartRepository.GetAllByUserIdAsync(pageNumber, pageSize, userId);
            return carts.Select(x => x.ToProductCartDto()).ToList();
        }

        public async Task<CartDto> GetByIdAsync(string userId, int productId)
        {
            var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = userId });
            if (string.IsNullOrEmpty(user.Id))
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

        public async Task<CartDto> UpdateAsync(string userId, int productId, CartDto cart)
        {
            var existingCart = await _cartRepository.UpdateAsync(userId, productId, cart.ToModel());
            if (existingCart == null)
            {
                throw new NotFoundException("Cart unit not found");
            }
            return existingCart.ToProductCartDto();
        }
    }
}
