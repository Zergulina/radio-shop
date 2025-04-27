using RadioShop.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Services
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync();
        Task<TagDto> GetByIdAsync(int id);
        Task<TagDto> CreateAsync(TagDto dto);
        Task<TagDto> UpdateAsync(int id, TagDto dto);
        Task<TagDto> DeleteAsync(int id);
    }
}
