using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IProductTagRepository
    {   
        Task<List<Tag>> GetAllTagsByProductIdAsync(int productId);
        Task<Tag?> CreateAsync(int tagId, int productId);
        Task<Tag?> DeleteAsync(int tagId, int productId);
        Task<bool> ExistsAsync(int tagId, int productId);
    }
}
