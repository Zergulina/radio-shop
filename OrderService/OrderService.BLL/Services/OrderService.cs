using MassTransit;
using MassTransit.Transports;
using OrderService.BLL.Dtos;
using OrderService.BLL.Exceptions;
using OrderService.BLL.Interfaces;
using OrderService.BLL.Mappers;
using OrderService.BLL.RabbitMQ.Messages;
using OrderService.DAL.Interfaces;

namespace OrderService.BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly UserGrpc.UserGrpcClient _userGrpcClient;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IPublishEndpoint publishEndpoint, UserGrpc.UserGrpcClient userGrpcClient)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
            _userGrpcClient = userGrpcClient;
        }

        public async Task AcceptAsync(int id, string userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (!order.UserId.Equals(userId))
            {
                throw new UnauthorizedException();
            }
            var acceptedOrder = await _orderRepository.AcceptAsync(id);
            if (acceptedOrder == null)
            {
                throw new NotFoundException("Order not found");
            }
            await _publishEndpoint.PublishBatch<AddOrderMessage>(order.Units.Select(x => new AddOrderMessage { Id = x.ProductId, OrderAmount = x.Amount }));
        }

        public async Task<bool> CheckDoesUserBoughtProduct(string userId, int productId)
        {
            return await _orderRepository.CheckDoesUserBoughtProduct(userId, productId);
        }

        public async Task<int> CountAsync(
            string? userId = null, 
            DateTime? startOrderedAt = null, 
            DateTime? endOrderedAt = null, 
            DateTime? startDeliveryDateTime = null, 
            DateTime? endDeliveryDateTime = null, 
            ulong? minUnitsAmount = null, 
            ulong? maxUnitsAmount = null, 
            decimal? minCost = null, 
            decimal? maxCost = null, bool? 
            isAccepted = null
            )
        {
            return await _orderRepository.CountAsync(userId, startOrderedAt, endOrderedAt, startDeliveryDateTime, endDeliveryDateTime, minUnitsAmount, maxUnitsAmount, minCost, maxCost, isAccepted);
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            try
            {
                var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = dto.UserId });
                if (string.IsNullOrEmpty(user.Id))
                {
                    throw new NotFoundException("User not found");
                }
                if ((await _productRepository.ExistsListAsync(dto.Units.Select(x => x.ProductId).ToList())).Count() > 0)
                {
                    throw new NotFoundException("Product not found");
                }
                dto.OrderedAt = DateTime.UtcNow;
                var created = (await _orderRepository.CreateAsync(dto.ToModel())).ToDto(user);
                return created;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<OrderDto> DeleteAcceptedAsync(int id, string userId)
        {
            var deletingOrder = await _orderRepository.DeleteAsync(id);
            if (deletingOrder == null)
            {
                throw new NotFoundException("Order not found");
            }
            if (!deletingOrder.UserId.Equals(userId))
            {
                throw new UnauthorizedException();
            }
            if (!deletingOrder.IsAccepted)
            {
                throw new OrderIsNotAcceptedException();
            }
            await _publishEndpoint.PublishBatch<RemoveOrderMessage>(deletingOrder.Units.Select(x => new RemoveOrderMessage { Id = x.ProductId, OrderAmount = x.Amount }));
            return deletingOrder.ToDto();
        }

        public async Task<OrderDto> DeleteAsync(int id)
        {
            var deletingOrder = await _orderRepository.DeleteAsync(id);
            if (deletingOrder == null)
            {
                throw new NotFoundException("Order not found");
            }
            await _publishEndpoint.PublishBatch<RemoveOrderMessage>(deletingOrder.Units.Select(x => new RemoveOrderMessage { Id = x.ProductId, OrderAmount = x.Amount }));
            return deletingOrder.ToDto();
        }

        public async Task<List<OrderDto>> GetAllAsync(
            int pageNumber = 1, 
            int pageSize = 20, 
            string? userId = null, 
            DateTime? startOrderedAt = null, 
            DateTime? endOrderedAt = null, 
            DateTime? startDeliveryDateTime = null, 
            DateTime? endDeliveryDateTime = null, 
            ulong? minUnitsAmount = null, 
            ulong? maxUnitsAmount = null, 
            decimal? minCost = null, 
            decimal? maxCost = null, 
            bool? isAccepted = null, 
            bool isDescending = false, 
            string? sortBy = null
            )
        {
            var orders = await _orderRepository.GetAllAsync(
                pageNumber,
                pageSize,
                userId,
                startOrderedAt,
                endOrderedAt,
                startDeliveryDateTime,
                endDeliveryDateTime,
                minUnitsAmount,
                maxUnitsAmount,
                minCost,
                maxCost,
                isAccepted,
                isDescending,
                sortBy
            );

            if (userId == null)
            {
                var request = new UserGrpcListRequest();
                orders.ForEach(x => request.Ids.Add(x.UserId));
                var users = await _userGrpcClient.GetUserListAsync(request);
                var userDict = users.Users.ToDictionary(x => x.Id);

                return orders.Select(order =>
                {
                    if (userDict.TryGetValue(order.UserId, out var user))
                    {
                        return order.ToDto(user);
                    }
                    return order.ToDto();
                }).ToList();
            }

            return orders.Select(x => x.ToDto()).ToList();
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                throw new NotFoundException("Order not found");
            }
            var user = await _userGrpcClient.GetUserAsync(new UserGrpcRequest { Id = existingOrder.UserId });
            if (string.IsNullOrEmpty(user.Id))
            {
                throw new NotFoundException("Order not found");
            }
            return existingOrder.ToDto(user);
        }
    }
}
