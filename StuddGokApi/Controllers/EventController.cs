using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using System.Collections.Generic;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Authorize]
    [HttpGet("User/{userId}", Name = "GetAllEvents")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents([FromRoute] int userId, [FromQuery] string? type=null,
        [FromQuery] DateTime? from=null, [FromQuery] DateTime? to=null)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        string gokstad_email = (string)HttpContext.Items["GokstadEmail"]!;

        IEnumerable<EventDTO>? events = await _eventService.GetEventsAsync(userId, type, from, to, user_id, role);
        if (events == null)
        {
            return Unauthorized("Du har ikke lov til å bruke denne routen.");
        }
        return Ok(events);
    }
}
