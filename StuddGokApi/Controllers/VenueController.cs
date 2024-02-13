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
    public async Task<ActionResult<IEnumerable<VenueDTO>>> GetAllVenues()
    {
        IEnumerable<VenueDTO> venues = await _venueService.GetAllVenuesAsync();
        return Ok(venues);
    }

}
