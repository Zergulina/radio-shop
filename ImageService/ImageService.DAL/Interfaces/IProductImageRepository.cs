using ImageService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.DAL.Interfaces
{
    public interface IProductImageRepository
    {
        Task<Image> GetByIdAsync(string id);
        Task<string> CreateAsync(Image image);
        Task DeleteAsync(string id);
    }
}
