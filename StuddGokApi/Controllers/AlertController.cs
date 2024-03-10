using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;
using System.Data;

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
    [HttpPut("User", Name = "UpdateUnseenAlertsForUser")]
    public async Task<ActionResult<IEnumerable<AlertDTO>?>> UpdateUnseenAlertsForUser()
    {
        int user_id = (int)HttpContext.Items["UserId"]!;

        IEnumerable<AlertDTO>? alerts = await _alertService.UpdateUnseenAlertsByUserIdAsync(user_id);
        if (alerts == null) return NotFound("Kunne ikke oppdatere varslingene.");
        return Ok(alerts);
    }

    [Authorize]
    [HttpPut(Name = "UpdateAlertsByAlertIds")]
    public async Task<ActionResult<IEnumerable<AlertDTO>?>> UpdateAlertsByAlertIds([FromBody] IEnumerable<int> alertIds)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<AlertDTO>? alerts =  await _alertService.UpdateAlertsByAlertIdsAsync(alertIds, user_id, role);
        if (alerts == null) return NotFound("Kunne ikke oppdatere varslingene.");
        return Ok(alerts);
    }

    [Authorize]
    [HttpPut("{id}", Name = "UpdateAlertByAlertId")]
    public async Task<ActionResult<AlertDTO?>> UpdateAlertById([FromRoute] int id)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        AlertDTO? alert = await _alertService.UpdateAlertByIdAsync(id, user_id);
        if (alert == null) return NotFound("Kunne ikke oppdatere varslingen. Er den slettet?");
        return Ok(alert);
    }
}
