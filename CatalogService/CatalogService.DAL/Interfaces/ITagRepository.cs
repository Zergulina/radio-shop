using System;
using CatalogService.DAL.Models;

namespace CatalogService.DAL.Interfaces;

public interface ITagRepository
{
    Task<Tag?> GetByIdAsync(int id);
    Task<List<Tag>> GetAllAsync();
    Task<Tag> CreateAsync(Tag tag);
    Task<Tag?> UpdateAsync(int id, Tag tag);
    Task<Tag?> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
