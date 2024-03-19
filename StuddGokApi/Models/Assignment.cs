using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class Assignment
{
    [Key] public int Id { get; set; }
    [ForeignKey("CourseImplementationId")] public int CourseImplementationId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public DateTime Deadline { get; set; }
    [Required] public bool Mandatory { get; set; }


    public virtual ICollection<AssignmentResult> AssignmentResults { get; set; } = new HashSet<AssignmentResult>();
    public virtual CourseImplementation? CourseImplementation { get; set; }
}
