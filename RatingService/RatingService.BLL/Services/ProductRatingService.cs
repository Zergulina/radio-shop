using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using RatingService.BLL.Dtos;
using RatingService.BLL.Exceptions;
using RatingService.BLL.Interfaces;
using RatingService.BLL.Mappers;
using RatingService.BLL.RabbitMQ.Messages;
using RatingService.DAL.Interfaces;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Services
{
    internal class ProductRatingService : IProductRatingService
    {
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly UserGrpc.UserGrpcClient _userGrpcClient;
        private readonly OrderGrpc.OrderGrpcClient _orderGrpcClient;
        public ProductRatingService(
            IProductRatingRepository productRatingRepository, 
            IProductRepository productRepository, 
            IPublishEndpoint publishEndpoint,
            UserGrpc.UserGrpcClient userGrpcClient,
            OrderGrpc.OrderGrpcClient orderGrpcClient
        )
        {
            _productRatingRepository = productRatingRepository;
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
            _userGrpcClient = userGrpcClient;
            _orderGrpcClient = orderGrpcClient; 
        }

        public async Task<int> CountByProductIdAsync(
            int productId, 
            byte? minRating = null, 
            byte? maxRating = null, 
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null
        )
        {
            if(! await _productRepository.ExistsAsync(productId))
            {
                throw new NotFoundException("Product not found");
            }

            return await _productRatingRepository.CountByProductIdAsync(productId, minRating, maxRating, startCreatedAt, endCreatedAt); ;
        }

        public async Task<int> CountByUserIdAsync(
            string userId, 
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            byte? minMyRating = null, 
            byte? maxMyRating = null, 
            byte? minRating = null, 
            byte? maxRating = null, 
            string? name = null, 
            string? tag = null, 
            DateTime? startCreatedAt = null, 
            DateTime? endCreatedAt = null
        )
        {
            if (! (await _userGrpcClient.UserExistsAsync(new UserGrpcRequest { Id = userId})).Exists)
            {
                throw new NotFoundException("User not found");
            }

            return await _productRatingRepository.CountByUserIdAsync(userId, minPrice, maxRating, minMyRating, maxMyRating, minRating, maxRating, name, tag, startCreatedAt, endCreatedAt);
        }

        public async Task<ProductRatingDto> CreateAsync(ProductRatingDto productRating)
        {
            if (!(await _userGrpcClient.UserExistsAsync(new UserGrpcRequest { Id = productRating.UserId })).Exists)
            {
                throw new NotFoundException("User not found");
            }
            if (!await _productRepository.ExistsAsync(productRating.ProductId))
            {
                throw new NotFoundException("Product not found");
            }
            if (await _productRatingRepository.ExistsAsync(productRating.UserId, productRating.ProductId))
            {
                throw new AlreadyExistsException("Rating already exists");
            }
            if (!(await _orderGrpcClient.CheckDoesUserBoughtProductAsync(new OrderGrpcRequest { ProductId = productRating.ProductId, UserId = productRating.UserId })).Check)
            {
                throw new UnauthorizedException();
            }
            productRating.CreatedAt = DateTime.UtcNow;
            var created = (await _productRatingRepository.CreateAsync(productRating.ToModel())).ToRatingDto();
            await _publishEndpoint.Publish<AddRatingMessage>(new AddRatingMessage { Id = created.ProductId, Rating = productRating.Rating });
            return created;
        }
        public async Task DeleteAsync(string userId, int productId)
        {
            var deletingRating = await _productRatingRepository.DeleteAsync(userId, productId);
            if (deletingRating == null)
            {
                throw new NotFoundException("Rating not found");
            }
            await _publishEndpoint.Publish<RemoveRatingMessage>(new RemoveRatingMessage { Id = productId, Rating = deletingRating.Rating });
        }

        public async Task<List<ProductRatingDto>> GetAllByProductIdAsync(
            int productId, 
            int pageNumber = 1, 
            int pageSize = 20, 
            byte? minRating = null, 
            byte? maxRating = null, 
            DateTime? startCreatedAt = null, 
            DateTime? endCreatedAt = null, 
            bool isDescending = false, 
            string? sortBy = null
        )
        {
            if (!await _productRepository.ExistsAsync(productId))
            {
                throw new NotFoundException("Product not found");
            }

            var ratings = await _productRatingRepository.GetAllByProductIdAsync(productId, pageNumber, pageSize, minRating, maxRating, startCreatedAt, endCreatedAt, isDescending, sortBy);
            var request = new UserGrpcListRequest();
            ratings.ForEach(x => request.Ids.Add(x.UserId));
            var users = await _userGrpcClient.GetUserListAsync(request);
            var userDict = users.Users.ToDictionary(x => x.Id);

            return ratings.Select(rating =>
            {
                if (userDict.TryGetValue(rating.UserId, out var user))
                {
                    return rating.ToUserRatingDto(user);
                }
                return rating.ToRatingDto();
            }).ToList();
        }

        public async Task<List<ProductRatingDto>> GetAllByUserIdAsync(
            string userId, 
            int pageNumber = 1, 
            int pageSize = 20, 
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            byte? minMyRating = null, 
            byte? maxMyRating = null, 
            byte? minRating = null, 
            byte? maxRating = null, 
            string? name = null, 
            string? tag = null, 
            DateTime? startCreatedAt = null, 
            DateTime? endCreatedAt = null, 
            bool isDescending = false, 
            string? sortBy = null
        )
        {
            if (!(await _userGrpcClient.UserExistsAsync(new UserGrpcRequest { Id = userId })).Exists)
            {
                throw new NotFoundException("User not found");
            }

            return (await _productRatingRepository.GetAllByUserIdAsync(userId, pageNumber, pageSize, minPrice, maxPrice, minMyRating, maxMyRating, minRating, maxRating, name, tag, startCreatedAt, endCreatedAt, isDescending, sortBy)).Select(x => x.ToProductRatingDto()).ToList();
        }
        public async Task<ProductRatingDto> UpdateAsync(string userId, int productId, ProductRatingDto productRating)
        {
            var prevRating = await _productRatingRepository.GetByIdAsync(userId, productId);
            if (prevRating == null)
            {
                throw new NotFoundException("Rating not found");
            }
            var rating = await _productRatingRepository.UpdateAsync(userId, productId, productRating.ToModel());
            if (rating == null)
            {
                throw new NotFoundException("Rating not found");
            }
            if (prevRating.Rating != rating.Rating)
            {
                await _publishEndpoint.Publish<RemoveRatingMessage>(new RemoveRatingMessage { Id = productId, Rating = prevRating.Rating });
                await _publishEndpoint.Publish<AddRatingMessage>(new AddRatingMessage { Id = productId, Rating = prevRating.Rating });
            }
            return rating.ToRatingDto();
        }
    }
}
