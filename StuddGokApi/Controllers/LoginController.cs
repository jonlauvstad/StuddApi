using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly ILogger<LoginController> _logger;
    public LoginController(ILoginService loginService, ILogger<LoginController> logger)
    {
        _loginService = loginService;
        _logger = logger;
    }

    [HttpPost(Name = "Login")]
    public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LoginController", "Login", $"/Login", "POST", "In", traceId);

        string? jwtToken = await _loginService.LoginAsync(loginDTO);
        if (jwtToken == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LoginController", "Login", $"/Login", "POST", "Out", traceId, BadRequest().StatusCode);

            return BadRequest("Invalid credentials"); 
        }

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LoginController", "Login", $"/Login", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(jwtToken);
    }

    [HttpGet(Name = "Testing")]
    public async Task<ActionResult<string>> Testing()
    {
        await Task.Delay(10);
        string role = $"{HttpContext.Items["Role"]}";
        return Ok(HttpContext.Items["UserId"] + " " + HttpContext.Items["GokstadEmail"] + " " + HttpContext.Items["Role"]);
    }
}
