using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuddGokApi.Models;

namespace StuddGokApi.Data;

public class StuddGokDbContext : DbContext
{
    public StuddGokDbContext(DbContextOptions<StuddGokDbContext> options) : base(options)
    {
    }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentResult> AssignmentResults { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseImplementation> CourseImplementations { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamImplementation> ExamImplementations { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<LectureVenue> LectureVenues { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ProgramCourse> ProgramCourses { get; set; }
    public DbSet<ProgramImplementation> ProgramImplementations { get; set; }
    public DbSet<ProgramLocation> ProgramLocations { get; set; }
    public DbSet<StudentProgram> StudentPrograms { get; set; }
    public DbSet<StudyProgram> StudyPrograms { get; set; }
    public DbSet<TeacherCourse> TeacherCourses { get; set; }
    public DbSet<TeacherProgram> TeacherPrograms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserExamImplementation> UserExamImplementations { get; set; }
    public DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
