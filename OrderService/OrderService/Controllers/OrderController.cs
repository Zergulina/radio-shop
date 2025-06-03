using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderService.BLL.Exceptions;
using OrderService.BLL.Interfaces;
using OrderService.Dtos.Order;
using OrderService.Mappers;

namespace OrderService.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderUnitService _orderUnitService;
        public OrderController(IOrderService orderService, IOrderUnitService orderUnitService)
        {
            _orderService = orderService;
            _orderUnitService = orderUnitService;
        }
        [Authorize]
        [HttpPost("my-orders")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto createDto)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                return Created("", (await _orderService.CreateAsync(createDto.ToDto(userId))).ToResponse());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetAllMyOrders([FromQuery] Queries.Order.GetAllQuery query)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                return Ok((await _orderService.GetAllAsync(
                    query.PageNumber,
                    query.PageSize,
                    userId,
                    query.StartOrderedAt,
                    query.EndOrderedAt,
                    query.StartDeliveryDateTime,
                    query.EndDeliveryDateTime,
                    query.MinUnitsAmount,
                    query.MaxUnitsAmount,
                    query.MinCost,
                    query.MaxCost,
                    query.IsAccepted,
                    query.IsDescending,
                    query.SortBy
                    )).Select(x => x.ToResponse()));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("my-orders/count")]
        public async Task<IActionResult> CountMyOrders([FromQuery] Queries.Order.CountQuery query)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                return Ok(await _orderService.CountAsync(
                    userId,
                    query.StartOrderedAt,
                    query.EndOrderedAt,
                    query.StartDeliveryDateTime,
                    query.EndDeliveryDateTime,
                    query.MinUnitsAmount,
                    query.MaxUnitsAmount,
                    query.MinCost,
                    query.MaxCost,
                    query.IsAccepted
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("my-orders/{id:int}/accept")]
        public async Task<IActionResult> Accept([FromRoute] int id)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                await _orderService.AcceptAsync(id, userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAll([FromQuery] Queries.Order.GetAllWithUserQuery query)
        {
            try
            {
                return Ok((await _orderService.GetAllAsync(
                    query.PageNumber,
                    query.PageSize,
                    query.UserId,
                    query.StartOrderedAt,
                    query.EndOrderedAt,
                    query.StartDeliveryDateTime,
                    query.EndDeliveryDateTime,
                    query.MinUnitsAmount,
                    query.MaxUnitsAmount,
                    query.MinCost,
                    query.MaxCost,
                    query.IsAccepted,
                    query.IsDescending,
                    query.SortBy
                    )).Select(x => x.ToResponse()));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders/count")]
        public async Task<IActionResult> Count([FromQuery] Queries.Order.CountWithUserQuery query)
        {
            try
            {
                return Ok(await _orderService.CountAsync(
                    query.UserId,
                    query.StartOrderedAt,
                    query.EndOrderedAt,
                    query.StartDeliveryDateTime,
                    query.EndDeliveryDateTime,
                    query.MinUnitsAmount,
                    query.MaxUnitsAmount,
                    query.MinCost,
                    query.MaxCost,
                    query.IsAccepted
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders/{orderId:int}/units")]
        public async Task<IActionResult> GetOrderUnits([FromRoute] int orderId, [FromQuery] Queries.OrderUnit.GetAllQuery query)
        {
            try
            {
                return Ok(await _orderUnitService.GetAllByOrderIdAsync(
                    orderId,
                    query.PageNumber,
                    query.PageSize,
                    null,
                    query.MinAmount,
                    query.MaxAmount,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinCost,
                    query.MaxCost,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag,
                    query.IsDescending,
                    query.SortBy
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders/{orderId:int}/units/count")]
        public async Task<IActionResult> CountOrderUnits([FromRoute] int orderId, [FromQuery] Queries.OrderUnit.CountQuery query)
        {
            try
            {
                return Ok(await _orderUnitService.CountByOrderIdAsync(
                    orderId,
                    null,
                    query.MinAmount,
                    query.MaxAmount,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinCost,
                    query.MaxCost,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("my-orders/{orderId:int}/units")]
        public async Task<IActionResult> GetMyOrderUnits([FromRoute] int orderId, [FromQuery] Queries.OrderUnit.GetAllQuery query)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                return Ok(await _orderUnitService.GetAllByOrderIdAsync(
                    orderId,
                    query.PageNumber,
                    query.PageSize,
                    userId,
                    query.MinAmount,
                    query.MaxAmount,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinCost,
                    query.MaxCost,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag,
                    query.IsDescending,
                    query.SortBy
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("my-orders/{orderId:int}/units/count")]
        public async Task<IActionResult> CountMyOrderUnits([FromRoute] int orderId, [FromQuery] Queries.OrderUnit.CountQuery query)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                return Ok(await _orderUnitService.CountByOrderIdAsync(
                    orderId,
                    userId,
                    query.MinAmount,
                    query.MaxAmount,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinCost,
                    query.MaxCost,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("orders/{orderId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int orderId)
        {
            try
            {
                await _orderService.DeleteAsync(orderId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpDelete("my-orders/{orderId:int}")]
        public async Task<IActionResult> DeleteMyOrder([FromRoute] int orderId)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                if (userId == null)
                {
                    return Unauthorized("User Id is not valid");
                }
                await _orderService.DeleteAcceptedAsync(orderId, userId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (OrderIsNotAcceptedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
