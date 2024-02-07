using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class Lecture
{
    [Key] public int Id { get; set; }
    [ForeignKey("CourseImplementationId")] public int CourseImplementationId { get; set; }
    [Required] public string Theme { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }

    public virtual ICollection<LectureVenue> LectureVenues { get; set; } = new HashSet<LectureVenue>();
    public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
    public virtual CourseImplementation? CourseImplementation { get; set; }
}
