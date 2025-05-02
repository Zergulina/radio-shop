using System;
using CatalogService.BLL.Dtos;

namespace CatalogService.BLL.Interfaces;

public interface ITagService
{
    Task<List<TagDto>> GetAllAsync();
    Task<TagDto> GetByIdAsync(int id);
    Task<TagDto> CreateAsync(TagDto dto);
    Task<TagDto> UpdateAsync(int id, TagDto dto);
    Task<TagDto> DeleteAsync(int id);
}
