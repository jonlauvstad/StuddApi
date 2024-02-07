using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class ProgramCourse
{
    [Key] public int Id { get; set; }
    [ForeignKey("ProgramImplementationId")] public int ProgramImplementationId { get; set; }
    [ForeignKey("CourseImplementationId")] public int CourseImplementationId { get; set; }

    public virtual ProgramImplementation? ProgramImplementation { get; set; }
    public virtual CourseImplementation? CourseImplementation { get; set;}
}
