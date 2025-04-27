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
    internal class RatingService : IRatingService
    {
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductOrderRepository _productOrderRepository;

        public RatingService(
            IProductRatingRepository productRatingService, 
            IUserRepository userRepository, 
            IProductRepository productRepository,
            IProductOrderRepository productOrderRepository
        )
        {
            _productRatingRepository = productRatingService;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
        }
        public async Task<int> CountByProductIdAsync(int productId, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null)
        {
            return await _productRatingRepository.CountByProductIdAsync(productId, minRating, maxRating, minDateTime, maxDateTime);
        }

        public async Task<int> CountByUserIdAsync(string userId, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, string? productName = null)
        {
            return await _productRatingRepository.CountByUserIdAsync(userId, minRating, maxRating, minDateTime, maxDateTime, productName);
        }

        public async Task<UserProductRatingDto> CreateAsync(UserProductRatingDto productRatingDto)
        {
            if (!await _productRepository.ExistsAsync(productRatingDto.ProductId))
            {
                throw new NotFoundException("Product not found");
            }
            if (!await _userRepository.ExistsAsync(productRatingDto.UserId))
            {
                throw new NotFoundException("User not found");
            }
            if (!await _productOrderRepository.CheckUserBoughtProduct(productRatingDto.UserId, productRatingDto.ProductId))
            {
                throw new UnauthorizedException();
            }

            return (await _productRatingRepository.CreateAsync(productRatingDto.ToModel())).ToUserProductRatingDto();
        }

        public async Task<UserProductRatingDto> DeleteAsync(string requestedUserLogin, int productId, string userId)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("User not found");
            }
            
            if (!requestedUser.Id.Equals(userId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            } 

            var productRating = await _productRatingRepository.DeleteAsync(productId, userId);
            if (productRating == null)
            {
                throw new NotFoundException("Product rating not found");
            }
            return productRating.ToUserProductRatingDto();
        }

        public async Task<List<ProductProductRatingDto>> GetAllByProductIdAsync(int productId, int pageNumber = 1, int pageSize = 20, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var productRatings = await _productRatingRepository.GetAllByProductIdAsync(productId, pageNumber, pageSize, minRating, maxRating, minDateTime, maxDateTime, isDescending, sortBy);
            return productRatings.Select(x => x.ToProductProductRatingDto()).ToList();
        }

        public async Task<List<UserProductRatingDto>> GetAllByUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var productRatings = await _productRatingRepository.GetAllByUserIdAsync(userId, pageNumber, pageSize, minRating, maxRating, minDateTime, maxDateTime, productName, isDescending, sortBy);
            return productRatings.Select(x => x.ToUserProductRatingDto()).ToList();
        }

        public async Task<UserProductRatingDto> GetByIdAsync(int productId, string userId)
        {
            var productRating = await _productRatingRepository.GetByIdAsync(productId, userId);
            if (productRating == null)
            {
                throw new NotFoundException("Product rating not found");
            }

            return productRating.ToUserProductRatingDto();
        }

        public async Task<UserProductRatingDto> UpdateAsync(int productId, string userId, UserProductRatingDto productRatingDto)
        {
            var productRating = await _productRatingRepository.UpdateAsync(productId, userId, productRatingDto.ToModel());
            if (productRating == null)
            {
                throw new NotFoundException("Product rating not found");
            }

            return productRating.ToUserProductRatingDto();
        }
    }
}
