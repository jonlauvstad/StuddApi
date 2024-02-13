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
}
