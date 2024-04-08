using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class CourseImpRepository : ICourseImpRepository
{
    private readonly StuddGokDbContext _dbContext;

    public CourseImpRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CourseImplementation?> GetCourseImpByIdAsync(int id)
    {
        IEnumerable<CourseImplementation> cis = await _dbContext.CourseImplementations.Where(x => x.Id == id).ToListAsync();

        CourseImplementation? ci = cis.FirstOrDefault();
        if (ci == null) { return null; }
       
        return ci;
    }

    public async Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate, 
        (string role, int userId)? user=null)
    {
        IEnumerable<CourseImplementation> cimps = await _dbContext.CourseImplementations
            .Where(x => startDate == null ? true : x.StartDate >= startDate)
            .Where(x => endDate == null ? true : x.EndDate >= endDate)
            .ToListAsync();

        if (user == null || user.Value.role == "admin")
        {
            return cimps.OrderBy(x => x.Id);
        }
        if (user.Value.role == "student")
        {
            cimps = await StudentCourseImps(user.Value.userId, cimps);
            return cimps.OrderBy(x => x.Id);
        }
        // KODE for 'teacher'
        cimps = await TeacherCourseImps(user.Value.userId, cimps);
        return cimps.OrderBy(x => x.Id);
    }


    private async Task<IEnumerable<CourseImplementation>> StudentCourseImps(int userId, IEnumerable<CourseImplementation> cimps)
    {
        IEnumerable<StudentProgram> studPrgms = await _dbContext.StudentPrograms.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> progImpIds = from item in studPrgms select item.ProgramImplementationId;
        IEnumerable<ProgramCourse> progCourses = await _dbContext.ProgramCourses
                                                    .Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync(); 
        IEnumerable<int> courseImpIds = from item in progCourses select item.CourseImplementationId;
        return cimps.Where(x => courseImpIds.Contains(x.Id));
    }

    private async Task<IEnumerable<CourseImplementation>> TeacherCourseImps(int userId, IEnumerable<CourseImplementation> cimps)
    {
        IEnumerable<TeacherProgram> teachPrgms = await _dbContext.TeacherPrograms.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> progImpIds = from item in teachPrgms select item.ProgramImplementationId;
        IEnumerable<ProgramCourse> progCourses = await _dbContext.ProgramCourses
                                                    .Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();

        IEnumerable<TeacherCourse> teachCourses = await _dbContext.TeacherCourses.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> courseImpIdsT = from item in teachCourses select item.CourseImplementationId;

        IEnumerable<int> courseImpIdsP = from item in progCourses select item.CourseImplementationId;
        return cimps.Where(x => courseImpIdsT.Contains(x.Id) || courseImpIdsP.Contains(x.Id));
    }

    public async Task<IEnumerable<int>> GetQualifiedStudentIdsAsync(int courseImpId)
    {
        // Getting all student(id)s following courseImp (via program)
        IEnumerable<int> progImpIds = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId)
                                            .Select(x => x.ProgramImplementationId).ToListAsync();
        IEnumerable<int> studentIds = await _dbContext.StudentPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId))
                                            .Select(x => x.UserId).ToListAsync();
        
        // Getting all student(id)s who has received "ikke godkjent" on Assignment
        IEnumerable<int> assignmentIds = await _dbContext.Assignments.Where(x => x.CourseImplementationId != courseImpId)
                                            .Select(x => x.Id).ToListAsync();
        IEnumerable<int> failedStudents = await _dbContext.AssignmentResults
                                            .Where(x => assignmentIds.Contains(x.AssignmentId) && x.Grade == "ikke godkjent")
                                            .Select(x => x.UserId).ToListAsync();

        // Returning the students that have not failed
        return studentIds.Where(x => !failedStudents.Contains(x));
    }

    public async Task<IEnumerable<User>> GetQualifiedStudentObjectsAsync(int courseImpId)
    {
        IEnumerable<int> qualifiedStudents = await GetQualifiedStudentIdsAsync(courseImpId);
        return await _dbContext.Users.Where(x => qualifiedStudents.Contains(x.Id)).ToListAsync();
    }
}
