using System;
using CatalogService.BLL.Dtos;

namespace CatalogService.BLL.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(
                int pageNumber = 1,
                int pageSize = 20,
                decimal? minPrice = null,
                decimal? maxPrice = null,
                byte? minRating = null,
                byte? maxRating = null,
                string? name = null,
                string? tag = null,
                bool isDescending = false,
                string? sortBy = null
            );
    Task<int> CountAsync(
        decimal? minPrice = null,
        decimal? maxPrice = null,
        byte? minRating = null,
        byte? maxRating = null,
        string? name = null,
        string? tag = null
    );
    Task<ProductDto> AddTagsAsync(int productId, params int[] tagIds);
    Task<ProductDto> RemoveTagsAsync(int productId, params int[] tagIds);
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductDto dto, ProductImageGrpcCreateRequest? imageDto);
    Task<ProductDto> UpdateAsync(int id, ProductDto dto);
    Task<ProductDto> DeleteAsync(int id);
}
