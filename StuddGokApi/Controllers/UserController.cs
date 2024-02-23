using Microsoft.AspNetCore.Mvc;
using StuddGokApi.Services.Interfaces;
using StuddGokApi.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace StuddGokApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null) return NotFound("User not found");

            return Ok(userDto);
        }

        [Authorize]
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(string? role=null)
        {
            return Ok(await _userService.GetUsersAsync(role));
        }
    }
}
