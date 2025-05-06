using CartService.BLL.Exceptions;
using CartService.BLL.Interfaces;
using CartService.Dtos.Cart;
using CartService.Mappers;
using CartService.Queries.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CartService.Controllers
{
    [Route("api")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("cart/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateCartRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }

            try
            {
                var cart = await _cartService.CreateAsync(createDto.ToDto(userId, productId));
                return Ok(cart.ToResponse());
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (AlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("cart")]
        [Authorize]
        public async Task<IActionResult> GetMyCart([FromQuery] GetAllQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }

            try
            {
                var carts = await _cartService.GetAllByUserIdAsync(query.PageNumber, query.PageSize, userId);
                return Ok(carts.Select(x => x.ToProductCartResponse()));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("cart/count")]
        [Authorize]
        public async Task<IActionResult> CountMyCart()
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }

            try
            {
                var count = await _cartService.CountByUserIdAsync(userId);
                return Ok(count);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("cart/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }

            try
            {
                await _cartService.DeleteAsync(userId, productId);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("users/{userId}/cart")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllQuery query, [FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var carts = await _cartService.GetAllByUserIdAsync(query.PageNumber, query.PageSize, userId);
                return Ok(carts);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("users/{userId}/cart/count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountByUserId([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var count = await _cartService.CountByUserIdAsync(userId);
                return Ok(count);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("products/{productId:int}/cart")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllByProductId([FromQuery] GetAllQuery query, [FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var carts = await _cartService.GetAllByProductIdAsync(query.PageNumber, query.PageSize, productId);
                return Ok(carts);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("products/{productId:int}/cart/count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountByProductId([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var count = await _cartService.CountByProductIdAsync(productId);
                return Ok(count);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
