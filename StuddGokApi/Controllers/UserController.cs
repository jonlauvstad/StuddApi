using Microsoft.AspNetCore.Mvc;
using StuddGokApi.Services.Interfaces;
using StuddGokApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using StuddGokApi.Models;

namespace StuddGokApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;   

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            string? traceId = System.Diagnostics.Activity.Current?.Id;
            _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                    "UserController", "Get", $"/User/{id}", "GET", "In", traceId);

            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null) 
            {
                _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                    "UserController", "Get", $"/User/{id}", "GET", "Out", traceId, NotFound().StatusCode);

                return NotFound("User not found");
            }
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "UserController", "Get", $"/User/{id}", "GET", "Out", traceId, Ok().StatusCode);
            return Ok(userDto);
        }

        [Authorize]
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(string? role=null)
        {
            string? traceId = System.Diagnostics.Activity.Current?.Id;
            _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                    "UserController", "GetUsers", $"/User", "GET", "In", traceId);

            IEnumerable<UserDTO> users = await _userService.GetUsersAsync(role);
            _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoUsers:{noVenues}",
                "UserController", "GetUsers", "/User", "GET", "Out", traceId, 400, users.Count() == 0);
            return Ok(users);
        }
    }
}
