using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class CourseImplementation
{
    [Key] public int Id { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [ForeignKey("CourseId")] public int CourseId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public string Semester { get; set; } = string.Empty;
    [Required] public string EndSemester { get; set; } = string.Empty;
    [Required] public int Year { get; set; }
    [Required] public int EndYear { get; set; }

    public virtual ICollection<ProgramCourse> ProgramCourses { get; set; } = new HashSet<ProgramCourse>();
    public virtual ICollection<TeacherCourse> TeacherCourses { get; set; } = new HashSet<TeacherCourse>();
    public virtual ICollection<Assignment> Assignments { get; set; } = new HashSet<Assignment>();
    public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
    public virtual ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    public virtual Course? Course { get; set; }

    
}
