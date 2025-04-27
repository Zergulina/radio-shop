using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Image> CreateAsync(Image image); 
        Task<Image?> DeleteAsync(int id);
    }
}
