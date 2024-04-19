using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class CourseImplementationDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Semester { get => StartDate.Month < 0 ? "V" : "H"; } 
    public string EndSemester { get => EndDate.Month < 0 ? "V" : "H"; }
    public int Year { get => StartDate.Year; }
    public int EndYear { get => EndDate.Year; }

    //public virtual ICollection<ProgramCourse> ProgramCourses { get; set; } = new HashSet<ProgramCourse>();
    //public virtual ICollection<TeacherCourse> TeacherCourses { get; set; } = new HashSet<TeacherCourse>();
    //public virtual ICollection<Assignment> Assignments { get; set; } = new HashSet<Assignment>();
    //public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
    //public virtual ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    //public virtual Course? Course { get; set; }
    public string CourseLink { get => $"/Course/{CourseId}"; }
    public string Link { get => $"/CourseImplementation/{Id}"; }
    public int NumStudents { get; set; }
    public string ProgramImplementationName { get; set; } = string.Empty;
    public string Teachers { get; set; } = string.Empty;
}
