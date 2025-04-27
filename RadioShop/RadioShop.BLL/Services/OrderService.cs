using RadioShop.BLL.Dtos;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.BLL.Mappers;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IOrderRepository orderRepository, IProductOrderRepository productOrderRepository, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task<int> CountAsync(string requestedUserLogin, string userId, ulong? minPrice = null, ulong? maxPrice = null, DateTime? startOrderDateTime = null, DateTime? endOrderDateTime = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            if (!requestedUser.Id.Equals(userId)) {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            return await _orderRepository.CountAsync(userId, minPrice, maxPrice, startOrderDateTime, endOrderDateTime);
        }

        public async Task<int> CountOrdersByProductIdAsync(int productId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null)
        { 
            return await _productOrderRepository.CountByProductIdAsync(productId, minAmount, maxAmount, minOrderDateTime, maxOrderDateTime);
        }

        public async Task<int> CountProductByOrderIdAsync(string requestedUserLogin, int orderId, ulong? minAmount = null, ulong? maxAmount = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (!requestedUser.Id.Equals(order.UserId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            return await _productOrderRepository.CountByOrderIdAsync(orderId, minAmount, maxAmount, minPrice, maxPrice, productName);
        }

        public async Task<OrderDto> CreateAsync(OrderDto orderDto)
        {
            var carts = await _cartRepository.GetAllByUserIdAsync(orderDto.UserId);
            carts = carts.Where(x => x.Amount > 0).ToList();
            if (carts.Count == 0)
            {
                throw new NotFoundException("Cart is empty!");
            }

            var order = await _orderRepository.CreateAsync(orderDto.ToModel());
            order.OrderDateTime = DateTime.UtcNow;
            foreach (var cart in carts)
            {
                await _productOrderRepository.CreateAsync(new ProductOrder { ProductId = cart.ProductId, OrderId = order.Id, Amount = cart.Amount});
            }

            await _cartRepository.DeleteAllByUserIdAsync(orderDto.UserId);

            return order.ToDto();
        }

        public async Task<OrderDto> DeleteAsync(int id)
        {
            var order = await _orderRepository.DeleteAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            return order.ToDto();
        }

        public async Task<List<OrderDto>> GetAllAsync(string requestedUserLogin, string userId, int pageNumber = 1, int pageSize = 20, ulong? minPrice = null, ulong? maxPrice = null, DateTime? startOrderDateTime = null, DateTime? endOrderDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            if (!requestedUser.Id.Equals(userId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            var orders = await _orderRepository.GetAllAsync(userId, pageNumber, pageSize, minPrice, maxPrice, startOrderDateTime, endOrderDateTime, isDescending, sortBy);
            return orders.Select(x => x.ToDto()).ToList();
        }

        public async Task<List<ProductProductOrderDto>> GetAllOrdersByProductIdAsync(int productId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var productOrders = await _productOrderRepository.GetAllByProductIdAsync(productId, pageNumber, pageSize, minAmount, maxAmount, minOrderDateTime, maxOrderDateTime, isDescending, sortBy);
            return productOrders.Select(x => x.ToProductProductOrderDto()).ToList();
        }

        public async Task<List<OrderProductOrderDto>> GetAllProductsByOrderIdAsync(string requestedUserLogin, int orderId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (!requestedUser.Id.Equals(order.UserId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            var productOrders = await _productOrderRepository.GetAllByOrderIdAsync(orderId, pageNumber, pageSize, minAmount, maxAmount, minPrice, maxPrice, productName, isDescending, sortBy);
            return productOrders.Select(x => x.ToOrderProductOrderDto()).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(string requestedUserLogin, int id)
        {
            var requestedUser = await _userRepository.GetByUserNameAsync(requestedUserLogin);
            if (requestedUser == null)
            {
                throw new NotFoundException("Requested user not found");
            }
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (!requestedUser.Id.Equals(order.UserId))
            {
                var requestedUserRoles = await _userRepository.GetRolesAsync(requestedUser);
                if (!requestedUserRoles.Contains("Admin"))
                {
                    throw new UnauthorizedException();
                }
            }

            return order.ToDto();
        }
    }
}
