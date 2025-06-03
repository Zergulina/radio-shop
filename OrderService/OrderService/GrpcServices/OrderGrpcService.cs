using Grpc.Core;
using OrderService.BLL.Interfaces;

namespace OrderService.GrpcServices
{
    public class OrderGrpcService : OrderGrpc.OrderGrpcBase
    {
        private readonly IOrderService _orderService;
        public OrderGrpcService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async override Task<CheckDoesUserBoughtProductGrpcResponse> CheckDoesUserBoughtProduct(OrderGrpcRequest request, ServerCallContext context)
        {
            var userId = request.UserId;
            var productid = request.ProductId;

            try
            {
                var check = await _orderService.CheckDoesUserBoughtProduct(userId, productid);
                return new CheckDoesUserBoughtProductGrpcResponse
                {
                    Check = check
                };
            }
            catch (Exception ex)
            {
                return new CheckDoesUserBoughtProductGrpcResponse { };
            }
        }
    }
}
