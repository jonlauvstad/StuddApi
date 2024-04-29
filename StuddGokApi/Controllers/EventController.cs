using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using StudentResource.Models.POCO;
using System.Collections.Generic;
using System.Data;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventController> _logger;

    public EventController(IEventService eventService, ILogger<EventController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("User/{userId}", Name = "GetAllEventsForUser")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEventsForUser([FromRoute] int userId, [FromQuery] string? type=null,
        [FromQuery] DateTime? from=null, [FromQuery] DateTime? to=null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "EventController", "GetAllEventsForUser", $"/Event/User/{userId}", "GET", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        string gokstad_email = (string)HttpContext.Items["GokstadEmail"]!;

        IEnumerable<EventDTO>? events = await _eventService.GetEventsAsync(userId, type, from, to, user_id, role);
        if (events == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "EventController", "GetAllEventsForUser", $"/Event/User/{userId}", "GET", "Out", traceId, NotFound().StatusCode);
            return Unauthorized("Du har ikke lov til å bruke denne routen.");
        }

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "EventController", "GetAllEventsForUser", $"/Event/User/{userId}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(events);
    }

    [Authorize]
    [HttpGet(Name = "GetAllEvents")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents([FromQuery] string? type = null,
        [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "EventController", "GetAllEvents", $"/Event", "GET", "In", traceId);
        
        return Ok(await _eventService.GetAllEventsAsync(type, from, to));

    }
}
