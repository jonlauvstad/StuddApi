using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class ProgramImplementation
{
    [Key] public int Id { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [ForeignKey("StudyProgramId")] public int StudyProgramId { get; set; }   
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public string Semester { get; set; } = string.Empty;
    [Required] public string EndSemester { get; set; } = string.Empty;
    [Required] public int Year { get; set; }
    [Required] public int EndYear { get; set; }

    public virtual ICollection<ProgramCourse> ProgramCourses { get; set; } = new HashSet<ProgramCourse>();
    public virtual ICollection<ProgramLocation> ProgramLocations { get; set; } = new HashSet<ProgramLocation>();
    public virtual ICollection<TeacherProgram> TeacherPrograms { get; set; } = new HashSet<TeacherProgram>();
    public virtual ICollection<StudentProgram> StudentPrograms { get; set; } = new HashSet<StudentProgram>();
    public virtual StudyProgram? StudyProgram { get; set; }

    /*
     *     def __init__(self, program_id, program_table, startdate, enddate):
        ProgramImplementation.count += 1
        self.id = ProgramImplementation.count
        self.program_id = program_id
        self.startdate = datetime.datetime.strptime(startdate, "%d.%m.%y")
        self.enddate = datetime.datetime.strptime(enddate, "%d.%m.%y")
        self.semester = "V" if self.startdate.month < 7 else "H"
        self.end_semester = "V" if self.enddate.month < 7 else "H"
        self.year = self.startdate.year % 100
        self.end_year = self.enddate.year % 100
        self.program = [item for item in program_table if item.id == program_id][0]     # Utenfor tabellen
        self.code = f"{self.program.code}{self.semester}{self.year}"
        self.name = f"{self.program.name} {self.semester}{self.year}"
        self.courses = []               # Course    via program_course_table    hjelpetabell
        self.teachers = []              # User      via teacher_program_table   hjelpetabell
        self.students = []              # User      via student_program_table   hjelpetabell
        self.locations = []             # Location  via program_location_table  hjelpetabell
    */
}
