using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
//[Route("api/v1/[controller]")]
[Route("api/v1")]
public class VenueController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [Authorize]
    //[HttpGet(Name = "GetAllVenues")]
    [HttpGet("Venue", Name = "GetAllVenues")]
    public async Task<ActionResult<IEnumerable<VenueDTO>>> GetAllVenues(DateTime? from = null, DateTime? to  = null)
    {
        (DateTime from, DateTime to)? availableFromTo = from == null || to == null ? null : (from.Value, to.Value);
        IEnumerable<VenueDTO> venues = await _venueService.GetAllVenuesAsync(availableFromTo);
        return Ok(venues);
    }
        
    [Authorize]
    //[HttpGet("{id}", Name = "GetVenueById")]
    [HttpGet("Venue/{id}", Name = "GetVenueById")]
    public async Task<ActionResult<VenueDTO>> GetVenueById(int id)
    {
        var venue = await _venueService.GetVenueByIdAsync(id);
        if (venue == null)
        {
            return NotFound();
        }
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("Venue", Name = "AddVenue")]
    public async Task<ActionResult<VenueDTO>> AddVenue([FromBody] VenueDTO venueDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        VenueDTO? venue = await _venueService.AddVenueAsync(venueDTO);
        if (venue == null) return NotFound($"We could unfortunately not add the venue to the database.");
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("Venue/{id}", Name = "DeleteVenue")]
    public async Task<ActionResult<VenueDTO>> DeleteVenue([FromRoute] int id)
    {
        VenueDTO? venue = await _venueService.DeleteVenueAsync(id);
        if (venue == null) return NotFound($"We could unfortunately not delete the venue from the database.");
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("Venue/{id}", Name = "UpdateVenue")]
    public async Task<ActionResult<VenueDTO>> UpdateVenue([FromRoute] int id, [FromBody] VenueDTO venueDTO)
    {
        VenueDTO? venue = await _venueService.UpdateVenueAsync(id, venueDTO);
        if (venue == null) return NotFound($"We could unfortunately not update the venue in the database.");
        return Ok(venue);
    }


    [Authorize]
    [HttpGet("Location", Name = "GetAllLocations")]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations()
    {
        return Ok(await _venueService.GetAllLocationsAsync());
    }
}
