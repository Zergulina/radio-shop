using RadioShop.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Services
{
    public interface ICartService
    {
        public Task<List<UserCartDto>> GetAllByUserIdAsync(
            string requestedUserLogin,
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null,
            bool isDescending = false,
            string? sortBy = null
        );

        public Task<int> CountByUserIdAsync(
            string requestedUserLogin,
            string userId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null
        );
        public Task<List<ProductCartDto>> GetAllByProductId(
            int productId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            bool isDescending = false,
            string? sortBy = null
        );
        public Task<int> CountByProductId(
            int productId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null
        );
        Task<List<UserCartDto>> GetAllByUserLoginAsync(
            string requestedUserLogin,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByUserLoginAsync(
            string requestedUserLogin, 
            ulong? minAmount = null, 
            ulong? maxAmount = null, 
            DateTime? startDateTime = null, 
            DateTime? endDateTime = null, 
            ulong? minPrice = null, 
            ulong? maxPrice = null, 
            string? productName = null
        );
        public Task<UserCartDto> GetByIdAsync(string requestedUserLogin, int productId, string userId);
        public Task<UserCartDto> CreateAsync(string requestedUserLogin, int productId, ProductCartDto cartDto);
        public Task<UserCartDto> UpdateAsync(string requestedUserLogin, int productId, ProductCartDto cartDto);
        public Task DeleteAsync(string requestedUserLogin, int productId);
        public Task CleanCartAsync(string requestedUserLogin);
    }
}
