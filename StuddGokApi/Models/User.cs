using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StuddGokApi.Models;

public class User
{
    [Key] public int Id { get; set; }
    [Required] public string GokstadEmail { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Email2 { get; set; } = string.Empty;
    [Required] public string Email3 { get; set; } = string.Empty;
    [Required] public string Role { get; set; } = string.Empty;
    [Required] public string Salt { get; set; } = string.Empty;
    [Required] public string HashedPassword { get; set; } = string.Empty;

    public virtual ICollection<TeacherProgram> TeacherPrograms { get; set; } = new HashSet<TeacherProgram>();
    public virtual ICollection<StudentProgram> StudentPrograms { get; set; } = new HashSet<StudentProgram>();
    public virtual ICollection<TeacherCourse> TeacherCourses { get; set; } = new HashSet<TeacherCourse>();
    public virtual ICollection<AssignmentResult> AssignmentResults { get; set; } = new HashSet<AssignmentResult>();
    public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
    public virtual ICollection<LectureVenue> LectureVenues { get; set; } = new HashSet<LectureVenue>();


    /*
     *     def __init__(self, gokstad_email, first_name, last_name, role, salt, hashed_pw,
                 email2=None, email3=None):
        User.count += 1
        self.id = User.count
        self.gokstad_email = gokstad_email
        self.first_name = first_name
        self.last_name = last_name
        self.email2 = email2
        self.email3 = email3
        self.role = role
        self.salt = salt
        self.hashed_pw = hashed_pw
        self.teacher_programs = []          # ProgramImplementation     via teacher_program_table   hjelpetabell
        self.student_programs = []          # ProgramImplementation     via student_program_table   hjelpetabell
        self.teacher_courses = []           # CourseImplementation      via teacher_program_table   hjelpetabell
        self.attendances = []               # Lecture                   via attendance_table        hjelpetabell
    */
}
