using RadioShop.BLL.Dtos;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.BLL.Mappers;
using RadioShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IProductTagRepository _productTagRepository;
        public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IImageRepository imageRepository, IProductTagRepository productTagRepository)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _imageRepository = imageRepository;
            _productTagRepository = productTagRepository;   
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
                if (!await _productRepository.ExistsAsync(tagId))
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

        public async Task<int> CountAsync(ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null)
        {
            return await _productRepository.CountAsync(minPrice, maxPrice, minRating, maxRating, name, tag);
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            if (dto.Image != null)
            {
                var image = await _imageRepository.CreateAsync(dto.ToImageFromDto());
                dto.ImageId = image.Id;
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

        public async Task<List<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 20, ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null, bool isDescending = false, string? sortBy = null)
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
                if (!await _productRepository.ExistsAsync(tagId))
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

        public async Task<ProductDto> UpdateImageByProductId(int productId, ImageDto imageDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (product.ImageId != null)
            {
                await _imageRepository.DeleteAsync(productId);
            }

            var image = await _imageRepository.CreateAsync(imageDto.ToModel());
            product.ImageId = image.Id;
            await _productRepository.UpdateAsync(productId, product);

            return product.ToDto();
        }
        public async Task<ProductDto> DeleteImageByProductId(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (product.ImageId == null)
            {
                throw new NotFoundException("Image not found");
            }

            await _imageRepository.DeleteAsync(productId);
            product.ImageId = null;
            await _productRepository.UpdateAsync(productId, product);

            return product.ToDto();
        }
    }
}
