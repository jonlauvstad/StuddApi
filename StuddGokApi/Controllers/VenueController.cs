using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;
using System.Diagnostics;
using System.Text.Json;

namespace StuddGokApi.Controllers;

[ApiController]
//[Route("api/v1/[controller]")]
[Route("api/v1")]
public class VenueController : ControllerBase
{
    private readonly IVenueService _venueService;
    private readonly ILogger<VenueController> _logger;

    public VenueController(IVenueService venueService, ILogger<VenueController> logger)
    {
        _venueService = venueService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("Venue", Name = "GetAllVenues")]
    public async Task<ActionResult<IEnumerable<VenueDTO>>> GetAllVenues(DateTime? from = null, DateTime? to  = null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "VenueController", "GetAllVenues", "/Venue", "GET", "In", traceId);

        (DateTime from, DateTime to)? availableFromTo = from == null || to == null ? null : (from.Value, to.Value);
        IEnumerable<VenueDTO> venues = await _venueService.GetAllVenuesAsync(availableFromTo);

        _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoVenues:{noVenues}",
            "VenueController", "GetAllVenues", "/Venue", "GET", "Out", traceId, 400, venues.Count()==0);
        return Ok(venues);
    }
        
    [Authorize]
    [HttpGet("Venue/{id}", Name = "GetVenueById")]
    public async Task<ActionResult<VenueDTO>> GetVenueById(int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "VenueController", "GetVenueById", $"/Venue/{id}", "GET", "In", traceId);
        var venue = await _venueService.GetVenueByIdAsync(id);
        if (venue == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "GetVenueById", $"/Venue/{id}", "GET", "Out", traceId, NotFound().StatusCode);
            return NotFound();
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "GetVenueById", $"/Venue/{id}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("Venue", Name = "AddVenue")]
    public async Task<ActionResult<VenueDTO>> AddVenue([FromBody] VenueDTO venueDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "VenueController", "AddVenue", "/Venue", "POST", "In", traceId);

        VenueDTO? venue = await _venueService.AddVenueAsync(venueDTO);
        if (venue == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "AddVenue", "/Venue", "GET", "Out", traceId, NotFound().StatusCode);
            return NotFound($"We could unfortunately not add the venue to the database.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "VenueController", "AddVenue", "/Venue", "POST", "Out", traceId, 400);
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("Venue/{id}", Name = "DeleteVenue")]
    public async Task<ActionResult<VenueDTO>> DeleteVenue([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "VenueController", "DeleteVenue", $"/Venue/{id}", "DELETE", "In", traceId);
        VenueDTO? venue = await _venueService.DeleteVenueAsync(id);
        if (venue == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "DeleteVenue", $"/Venue/{id}", "DELETE", "Out", traceId, NotFound().StatusCode);

            return NotFound($"We could unfortunately not delete the venue from the database.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "DeleteVenue", $"/Venue/{id}", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(venue);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("Venue/{id}", Name = "UpdateVenue")]
    public async Task<ActionResult<VenueDTO>> UpdateVenue([FromRoute] int id, [FromBody] VenueDTO venueDTO)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "VenueController", "UpdateVenue", $"/Venue/{id}", "PUT", "In", traceId);

        VenueDTO? venue = await _venueService.UpdateVenueAsync(id, venueDTO);
        if (venue == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "VenueController", "UpdateVenue", $"/Venue/{id}", "PUT", "Out", traceId, NotFound().StatusCode);

            return NotFound($"We could unfortunately not update the venue in the database.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "VenueController", "UpdateVenue", $"/Venue/{id}", "PUT", "Out", traceId, Ok().StatusCode);
        return Ok(venue);
    }


    [Authorize]
    [HttpGet("Location", Name = "GetAllLocations")]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations()
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "VenueController", "GetAllLocations", "/Location", "GET", "In", traceId);

        var locations = await _venueService.GetAllLocationsAsync();
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoLocations:{noVenues}",
            "VenueController", "GetAllLocations", "/Location", "GET", "Out", traceId, 400, locations.Count() == 0);
        return Ok(locations);
    }
}
