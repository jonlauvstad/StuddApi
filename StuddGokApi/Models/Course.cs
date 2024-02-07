using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class Course
{
    [Required] public int Id { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public decimal Points { get; set; }
    [Required] public string Category {  get; set; } = string.Empty;
    [Required] public bool TeachCours { get; set; }
    [Required] public bool DiplomaCours { get; set; }
    [Required] public bool ExamCours { get; set; }

    public virtual ICollection<CourseImplementation> CoursesImplementations { get; set; } = new HashSet<CourseImplementation>();
}
