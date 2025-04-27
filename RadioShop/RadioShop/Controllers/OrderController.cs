using Microsoft.AspNetCore.Mvc;
using RadioShop.BLL.Interfaces.Services;

namespace RadioShop.WEB.Controllers
{
    [Route("api/")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
    }
}
