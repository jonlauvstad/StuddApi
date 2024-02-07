using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class TeacherCourse
{
    [Key] public int Id { get; set; }
    [ForeignKey("CourseImplementationId")] public int CourseImplementationId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public virtual CourseImplementation? CourseImplementation { get; set; }
    public virtual User? User { get; set; }
}
