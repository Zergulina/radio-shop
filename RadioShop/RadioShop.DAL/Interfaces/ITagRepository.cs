using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task<Tag> CreateAsync(Tag tag);
        Task<Tag?> UpdateAsync(int id, Tag tag);
        Task<Tag?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
