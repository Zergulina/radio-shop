using System;
using CatalogService.DAL.Models;

namespace CatalogService.DAL.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync(
        int pageNumber = 1,
        int pageSize = 20,
        ulong? minPrice = null,
        ulong? maxPrice = null,
        byte? minRating = null,
        byte? maxRating = null,
        string? name = null,
        string? tag = null,
        bool isDescending = false,
        string? sortBy = null
    );
    Task<int> CountAsync(
        ulong? minPrice = null,
        ulong? maxPrice = null,
        byte? minRating = null,
        byte? maxRating = null,
        string? name = null,
        string? tag = null
    );
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product product);
    Task<Product?> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
