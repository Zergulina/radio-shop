using ImageService.BLL.Dtos;
using ImageService.BLL.Interfaces;
using ImageService.BLL.Mappers;
using ImageService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.BLL.Services
{
    internal class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<string> CreateAsync(ImageDto image)
        {
            return await _productImageRepository.CreateAsync(image.ToModel());
        }

        public async Task DeleteAsync(string id)
        {
            await _productImageRepository.DeleteAsync(id);
        }

        public async Task<ImageDto> GetByIdAsync(string id)
        {
            return (await _productImageRepository.GetByIdAsync(id)).ToDto();
        }
    }
}
