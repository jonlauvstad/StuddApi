namespace StuddGokApi.DTOs;

public class LectureVenueDTO
{
    public int LectureId { get; set; }
    public int VenueId { get; set; }
    public string VenueName { get; set; } = string.Empty;
    public int VenueCapacity { get; set; }
}
