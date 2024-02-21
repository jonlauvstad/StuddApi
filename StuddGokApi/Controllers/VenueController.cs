using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VenueController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [Authorize]
    [HttpGet(Name = "GetAllVenues")]
    public async Task<ActionResult<IEnumerable<VenueDTO>>> GetAllVenues(DateTime? from = null, DateTime? to  = null)
    {
        (DateTime from, DateTime to)? availableFromTo = from == null || to == null ? null : (from.Value, to.Value);
        IEnumerable<VenueDTO> venues = await _venueService.GetAllVenuesAsync(availableFromTo);
        return Ok(venues);
    }

}
