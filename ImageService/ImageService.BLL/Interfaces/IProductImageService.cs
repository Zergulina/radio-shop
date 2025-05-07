using ImageService.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.BLL.Interfaces
{
    public interface IProductImageService
    {
        Task<ImageDto> GetByIdAsync(string id);
        Task<string> CreateAsync(ImageDto image);
        Task DeleteAsync(string id);
    }
}
