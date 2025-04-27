using RadioShop.BLL.Dtos;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.BLL.Mappers;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Services
{
    internal class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        public CartService(IProductRepository productRepository, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public async Task CleanCartAsync(string requestedUserLogin)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }

            await _cartRepository.DeleteAllByUserIdAsync(requestedUser.Id);
        }

        public async Task<int> CountByProductId(int productId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            return await _cartRepository.CountByProductIdAsync(productId, minAmount, maxAmount, startDateTime, endDateTime);
        }

        public async Task<int> CountByUserIdAsync(string requestedUserLogin, string userId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            if (!requestedUser.Id.Equals(userId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            return await _cartRepository.CountByUserIdAsync(userId, minAmount, maxAmount, startDateTime, endDateTime, minPrice, maxPrice, productName);
        }

        public async Task<int> CountByUserLoginAsync(string requestedUserLogin, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }

            return await _cartRepository.CountByUserIdAsync(requestedUser.Id, minAmount, maxAmount, startDateTime, endDateTime, minPrice, maxPrice, productName);
        }

        public async Task<UserCartDto> CreateAsync(string requestedUserLogin, int productId,  ProductCartDto cartDto)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            if (!await _productRepository.ExistsAsync(productId))
            {
                throw new NotFoundException("Product not found");
            }

            cartDto.DateTime = DateTime.UtcNow;
            cartDto.UserId = requestedUser.Id;
            cartDto.ProductId = productId;
            await _cartRepository.CreateAsync(cartDto.ToModel());
            return (await _cartRepository.GetByIdAsync(productId, requestedUser.Id)).ToUserCartDto();
        }

        public async Task DeleteAsync(string requestedUserLogin, int productId)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }

            var cart = await _cartRepository.DeleteAsync(productId, requestedUser.Id);
            if (cart == null)
            {
                throw new NotFoundException("Cart not found");
            }
        }

        public async Task<List<ProductCartDto>> GetAllByProductId(int productId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var carts = await _cartRepository.GetAllByProductIdAsync(productId, pageNumber, pageSize, minAmount, maxAmount, startDateTime, endDateTime, isDescending, sortBy);
            return carts.Select(x => x.ToProductCartDto()).ToList();
        }

        public async Task<List<UserCartDto>> GetAllByUserIdAsync(string requestedUserLogin, string userId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            if (!requestedUser.Id.Equals(userId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            var carts = await _cartRepository.GetAllByUserIdAsync(userId, pageNumber, pageSize, minAmount, maxAmount, startDateTime, endDateTime, minPrice, maxPrice, productName, isDescending, sortBy);
            return carts.Select(x => x.ToUserCartDto()).ToList();
        }

        public async Task<List<UserCartDto>> GetAllByUserLoginAsync(string requestedUserLogin, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }

            var carts = await _cartRepository.GetAllByUserIdAsync(requestedUser.Id, pageNumber, pageSize, minAmount, maxAmount, startDateTime, endDateTime, minPrice, maxPrice, productName, isDescending, sortBy);
            return carts.Select(x => x.ToUserCartDto()).ToList();
        }

        public async Task<UserCartDto> GetByIdAsync(string requestedUserLogin, int productId, string userId)
        {
            var cart = await _cartRepository.GetByIdAsync(productId, userId);
            if (cart == null)
            {
                throw new NotFoundException("Product not found in cart");
            }
            return cart.ToUserCartDto();
        }

        public async Task<UserCartDto> UpdateAsync(string requestedUserLogin, int productId, ProductCartDto cartDto)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }

            var cart = await _cartRepository.UpdateAsync(productId, requestedUser.Id, cartDto.ToModel());

            if (cart == null)
            {
                throw new NotFoundException("Product not found in cart");
            }
            return cart.ToUserCartDto();
        }
    }
}
