using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class LectureVenue
{
    [Key] public int Id { get; set; }
    [ForeignKey("LectureId")] public int LectureId { get; set; }
    [ForeignKey("VenueId")] public int VenueId { get; set; }

    public virtual Lecture? Lecture { get; set; }
    public virtual Venue? Venue { get; set; }
}
