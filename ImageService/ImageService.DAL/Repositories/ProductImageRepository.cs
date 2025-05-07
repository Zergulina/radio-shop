using ImageService.DAL.Data;
using ImageService.DAL.Interfaces;
using ImageService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.DAL.Repositories
{
    internal class ProductImageRepository: IProductImageRepository
    {
        private readonly MinioContext _minioContext;

        public ProductImageRepository(MinioContext minioContext)
        {
            _minioContext = minioContext;
        }

        public async Task<string> CreateAsync(Image image)
        {
            string id = Guid.NewGuid().ToString();
            await _minioContext.UploadFileAsync("ProductImages", id, image.ImageData, image.ImageType);
            return id;
        }

        public async Task DeleteAsync(string id)
        {
            await _minioContext.DeleteFileAsync("ProductImages", id);
        }

        public async Task<Image> GetByIdAsync(string id)
        {
           var (imageData, imageType) = await _minioContext.GetFileAsync("ProductImages", id);
            return new Image
            {
                Id = id,
                ImageData = imageData,
                ImageType = imageType
            };
        }
    }
}
