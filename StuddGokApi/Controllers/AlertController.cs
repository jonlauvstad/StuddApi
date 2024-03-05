using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AlertController : ControllerBase
{
    private readonly IAlertService _alertService;
    private readonly ILogger<AlertController> _logger;

    public AlertController(IAlertService alertService, ILogger<AlertController> logger)
    {
        _alertService = alertService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("User/{userId}", Name = "GetAllAlertsForUser")]
    public async Task<ActionResult<IEnumerable<AlertDTO>>> GetAllAlertsForUser([FromRoute] int userId, [FromQuery] bool seen)
    {
        return Ok(await _alertService.GetAlertsByUserIdAsync(userId, seen));
    }

    [Authorize]
    [HttpPut(Name = "UpdateAlerts")]
    public async Task<ActionResult<IEnumerable<AlertDTO>?>> UpdateAlertsByAlertIds([FromBody] IEnumerable<int> alertIds)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<AlertDTO>? alerts =  await _alertService.UpdateAlertsByAlertIdsAsync(alertIds, user_id, role);
        if (alerts == null) return NotFound("Kunne ikke oppdatere varslingene.");
        return Ok(alerts);
    }
}
