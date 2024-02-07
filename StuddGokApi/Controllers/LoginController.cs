using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost(Name = "Login")]
    public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        string? jwtToken = await _loginService.LoginAsync(loginDTO);
        if (jwtToken == null) { return BadRequest("Invalid credentials"); }
        return Ok(jwtToken);
    }

    [HttpGet(Name = "Testing")]
    public async Task<ActionResult<string>> Testing()
    {
        string role = $"{HttpContext.Items["Role"]}";
        return Ok(HttpContext.Items["UserId"] + " " + HttpContext.Items["GokstadEmail"] + " " + HttpContext.Items["Role"]);
    }
}
