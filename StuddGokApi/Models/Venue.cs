using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class Venue
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [ForeignKey("LocationId")] public int LocationId { get; set; }
    [Required] public string StreetAddress { get; set; } = string.Empty;
    [Required] public int PostCode { get; set; }
    [Required] public string City { get; set; } = string.Empty;
    [Required] public int Capacity { get; set; }

    public virtual ICollection<LectureVenue> LectureVenues { get; set; } = new HashSet<LectureVenue>();
    public virtual ICollection<ExamImplementation> ExamImplementations { get; set; } = new HashSet<ExamImplementation>();
    public virtual Location? Location { get; set; }
}
