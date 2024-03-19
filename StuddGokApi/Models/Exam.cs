using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class Exam
{
    [Key] public int Id { get; set; }
    [ForeignKey("CourseImplementationId")] public int CourseImplementationId { get; set; }
    [Required] public string Category { get; set; } = string.Empty;
    [Required] public decimal DurationHours { get; set; }
    [Required] public DateTime PeriodStart { get; set; }
    [Required] public DateTime PeriodEnd { get; set; }

    // Forgot s at the end
    public virtual ICollection<ExamImplementation> ExamImplementation { get; set; } = new HashSet<ExamImplementation>();
    public virtual ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
    public virtual CourseImplementation? CourseImplementation { get; set; }
}
