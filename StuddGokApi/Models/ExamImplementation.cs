using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class ExamImplementation
{
    [Key] public int Id { get; set; }
    [ForeignKey("ExamId")] public int ExamId { get; set; }
    [ForeignKey("VenueId")] public int VenueId { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }

    public virtual Exam? Exam { get; set; }
    public virtual Venue? Venue { get; set; }
    public virtual ICollection<UserExamImplementation> UserExamImplementation { get; set; } = new HashSet<UserExamImplementation>();
}
