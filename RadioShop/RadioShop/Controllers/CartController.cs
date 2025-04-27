

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.WEB.Dtos.Cart;
using RadioShop.WEB.Mappers;
using RadioShop.WEB.Queries.Cart;
using System.Security.Claims;

namespace RadioShop.WEB.Controllers
{
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) {
            _cartService = cartService;
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAllByUserLogin([FromQuery] GetAllByUserIdQuery query)
        {
            try
            {
                return Ok((await _cartService.GetAllByUserLoginAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), query.PageNumber, query.PageSize, query.MinAmount, query.MaxAmount, query.StartDateTime,query.EndDateTime, query.MinPrice, query.MaxPrice, query.ProductName, query.IsDescending, query.SortBy)).ToUserCartResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me/count")]
        [Authorize]
        public async Task<IActionResult> CountByUserLogin([FromRoute] int productId, [FromQuery] CountByUserIdQuery query)
        {
            try
            {
                return Ok(await _cartService.CountByUserLoginAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), query.MinAmount, query.MaxAmount, query.StartDateTime, query.EndDateTime, query.MinPrice, query.MaxPrice, query.ProductName));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("me/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateCartRequestDto createRequestDto)
        {
            try
            {
                return Ok((await _cartService.CreateAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), productId, createRequestDto.ToProductCartDto())).ToUserCartResponseUnitDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("me/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] CreateCartRequestDto createRequestDto)
        {
            try
            {
                return Ok((await _cartService.UpdateAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), productId, createRequestDto.ToProductCartDto())).ToUserCartResponseUnitDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("me/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            try
            {
                await _cartService.DeleteAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), productId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
