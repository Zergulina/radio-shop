using System;
using CatalogService.BLL.Dtos;
using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Interfaces;
using CatalogService.BLL.Mappers;
using CatalogService.DAL.Interfaces;
using MassTransit;

namespace CatalogService.BLL.Services;

internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly ProductImageGrpc.ProductImageGrpcClient _productImageGrpcClient;
        public ProductService(
            IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, ProductImageGrpc.ProductImageGrpcClient productImageGrpcClient)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _productImageGrpcClient = productImageGrpcClient;
        }

        public async Task<ProductDto> AddTagsAsync(int productId, params int[] tagIds)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            foreach (var tagId in tagIds)
            {
                if (!await _tagRepository.ExistsAsync(tagId))
                {
                    throw new NotFoundException("Tag not found");
                }
            }

            foreach (var tagId in tagIds)
            {
                if (await _productTagRepository.ExistsAsync(tagId, productId))
                {
                    throw new NotFoundException("Product already has this tag");
                }
                await _productTagRepository.CreateAsync(tagId, productId);
            }

            return product.ToDto();
        }

        public async Task<int> CountAsync(decimal? minPrice = null, decimal? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null)
        {
            return await _productRepository.CountAsync(minPrice, maxPrice, minRating, maxRating, name, tag);
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto, ProductImageGrpcCreateRequest? imageDto)
        {
        string? imageId  = null;
        if (imageDto != null)
        {
            var response = await _productImageGrpcClient.CreateAsync(imageDto);
            if (!response.Success)
            {
                throw new Exception();
            }
            dto.ImageId = response.Id;
        }
        var product = await _productRepository.CreateAsync(dto.ToModel());
            return product.ToDto();
        }

        public async Task<ProductDto> DeleteAsync(int id)
        {
            var product = await _productRepository.DeleteAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            return product.ToDto();
        }

        public async Task<List<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 20, decimal? minPrice = null, decimal? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null, bool isDescending = false, string? sortBy = null)
        {
            var products = await _productRepository.GetAllAsync(pageNumber, pageSize, minPrice, maxPrice, minRating, maxRating, name, tag, isDescending, sortBy);

            return products.Select(x => x.ToDto()).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            return product.ToDto();
        }

        public async Task<ProductDto> RemoveTagsAsync(int productId, params int[] tagIds)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            foreach (var tagId in tagIds)
            {
                if (!await _tagRepository.ExistsAsync(tagId))
                {
                    throw new NotFoundException("Tag not found");
                }
            }

            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId);
                if (!product.Tags.Contains(tag))
                {
                    throw new NotFoundException("Tag not found");
                }
            }

            foreach (var tagId in tagIds)
            {
                await _productTagRepository.DeleteAsync(tagId, productId);
            }

            return product.ToDto();
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductDto dto)
        {
            var product = await _productRepository.UpdateAsync(id, dto.ToModel());
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            return product.ToDto();
        }
    }