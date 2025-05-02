using System;
using CatalogService.DAL.Models;

namespace CatalogService.DAL.Interfaces;

public interface IProductTagRepository
{
        Task<List<Tag>> GetAllTagsByProductIdAsync(int productId);
        Task<Tag?> CreateAsync(int tagId, int productId);
        Task<Tag?> DeleteAsync(int tagId, int productId);
        Task<bool> ExistsAsync(int tagId, int productId);
}
