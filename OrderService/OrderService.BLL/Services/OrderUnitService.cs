using OrderService.BLL.Dtos;
using OrderService.BLL.Exceptions;
using OrderService.BLL.Interfaces;
using OrderService.BLL.Mappers;
using OrderService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services
{
    internal class OrderUnitService : IOrderUnitService
    {
        private readonly IOrderUnitRepository _orderUnitRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderUnitService(IOrderUnitRepository orderUnitRepository, IOrderRepository orderRepository)
        {
            _orderUnitRepository = orderUnitRepository;
            _orderRepository = orderRepository;
        }
        public async Task<int> CountByOrderIdAsync(
            int orderId,
            string? userId = null,
            ulong? minAmount = null, 
            ulong? maxAmount = null, 
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            decimal? minCost = null, 
            decimal? maxCost = null, 
            byte? minRating = null, 
            byte? maxRating = null, 
            string? name = null, 
            string? tag = null
            )
        {
            if (userId != null)
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order not found");
                }
                if (!order.UserId.Equals(userId))
                {
                    throw new UnauthorizedException();
                }
            }
            else {
                if (!await _orderRepository.ExistsAsync(orderId))
                {
                    throw new NotFoundException("Order not found");
                }
            }
            return await _orderUnitRepository.CountByOrderIdAsync(
                orderId,
                minAmount,
                maxAmount,
                minPrice,
                maxPrice,
                minCost,
                maxCost,
                minRating,
                maxRating,
                name,
                tag
                );
        }

        public async Task<List<OrderUnitDto>> GetAllByOrderIdAsync(
            int orderId, 
            int pageNumber = 1, 
            int pageSize = 20,
            string? userId = null,
            ulong? minAmount = null, 
            ulong? maxAmount = null, 
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            decimal? minCost = null, 
            decimal? maxCost = null, 
            byte? minRating = null, 
            byte? maxRating = null, 
            string? name = null, 
            string? tag = null, 
            bool isDescending = false, 
            string? sortBy = null
            )
        {
            if (userId != null)
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order not found");
                }
                if (!order.UserId.Equals(userId))
                {
                    throw new UnauthorizedException();
                }
            }
            if (!await _orderRepository.ExistsAsync(orderId))
            {
                throw new NotFoundException("Order not found");
            }
            return (await _orderUnitRepository.GetAllByOrderIdAsync(
                orderId,
                pageNumber,
                pageSize,
                minAmount,
                maxAmount,
                minPrice,
                maxPrice,
                minCost,
                maxCost,
                minRating,
                maxRating,
                name,
                tag,
                isDescending,
                sortBy
                )).Select(x => x.ToProductDto()).ToList();
        }
    }
}
