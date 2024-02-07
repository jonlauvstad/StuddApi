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

    /*
     *     def __init__(self, course_id, course_table, startdate, enddate):
        CourseImplementation.count += 1
        self.id = CourseImplementation.count
        self.course_id = course_id
        self.startdate = datetime.datetime.strptime(startdate, "%d.%m.%y")
        self.enddate = datetime.datetime.strptime(enddate, "%d.%m.%y")
        self.semester = "V" if self.startdate.month < 7 else "H"
        self.end_semester = "V" if self.enddate.month < 7 else "H"
        self.year = self.startdate.year % 100
        self.end_year = self.enddate.year % 100
        self.course = [item for item in course_table if item.id == course_id][0]  # Utenfor tabellen
        self.code = f"{self.course.code}{self.semester}{self.year}"
        self.name = f"{self.course.name} {self.semester}{self.year}"
        self.programs = []              # Program       via program_course_table    hjelpetabell
        self.teachers = []              # User          via teacher_course_table    hjelpetabell
        self.exam = []                  # Exam          via exam_table              (exam har ett course)
        self.assignments = []           # Assignment    via assignment_table        (assignment har ett course)
    */
}
