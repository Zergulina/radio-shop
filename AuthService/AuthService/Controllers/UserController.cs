using AuthService.BLL.Exceptions;
using AuthService.BLL.Interfaces.Services;
using AuthService.Dtos.User;
using AuthService.Mappers;
using AuthService.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RadioShop.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.CreateAsync(createDto.ToUserDto());
                return Created("", user.ToCreatedFromUserDto());
            }
            catch (UserNameIsOccupiedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserCreationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loginResp = await _userService.LoginAsync(loginDto.ToUserDto());
                return Ok(loginDto.ToAuthorizedFromLoginDto(loginResp.Item1, loginResp.Item2));
            }
            catch (IncorrectUserNameOrPasswordException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] UserGetAllQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok((await _userService.GetAllAsync(query.PageNumber, query.PageSize, query.Name, query.SortBy, query.IsDescending)).Select(x => x.ToResponseFromUserDto()).ToList());
        }

    }
}
