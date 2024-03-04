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

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Authorize]
    [HttpGet("User/{userId}", Name = "GetAllEventsForUser")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEventsForUser([FromRoute] int userId, [FromQuery] string? type=null,
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

    [Authorize]
    [HttpGet(Name = "GetAllEvents")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents([FromQuery] string? type = null,
        [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        return Ok(await _eventService.GetAllEventsAsync(type, from, to));
        //if (events == null)
        //{
        //    return NotFound("Du har ikke lov til å bruke denne routen.");
        //}
        //return Ok(events);
    }
}
