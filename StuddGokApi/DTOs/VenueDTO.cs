using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class VenueDTO
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public int PostCode { get; set; }
    public string City { get; set; } = string.Empty;
    public int Capacity { get; set; }
    

    public string LocationName { get; set; } = string.Empty;
    public string Link { get => $"/Venue/{Id}"; }
    public Dictionary<string, string> Links
    {
        get => new Dictionary<string, string>()
        {
            { "Location", $"/Location/{LocationId}" }
        };
    }
}