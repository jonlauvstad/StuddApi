using StuddGokApi.Models;

namespace StuddGokApi.DTOs;

public class EventDTO
{
    public DateTime Time { get; set; }
    public int UnderlyingId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string TypeEng { get; set; } = string.Empty;
    public int CourseImplementationId { get; set; }
    public string CourseImpCode { get; set; } = string.Empty;
    public string CourseImpName { get; set; } = string.Empty;
    public CourseImplementation? CourseImplementation { get; set; }
    public string CourseImplementationLink { get => $"/CourseImplementation/{CourseImplementationId}"; }
    public string Link { get => $"/{TypeEng}/{UnderlyingId}"; }
    public DateTime TimeEnd { get; set; }
    public int VenueId { get; set; }
    public string VenueName { get; set; } = string.Empty;
    public int VenueCapacity { get; set; }
}
