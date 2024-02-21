using Microsoft.AspNetCore.Mvc;
using StuddGokApi.Services.Interfaces;
using StuddGokApi.DTOs;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null) return NotFound("User not found");

            return Ok(userDto);
        }
    }
}
