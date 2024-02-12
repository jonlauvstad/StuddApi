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

    public async Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate)
    {
        IEnumerable<CourseImplementation> cimps = await _dbContext.CourseImplementations
            .Where(x => startDate == null ? true : x.StartDate >= startDate)
            .Where(x => endDate == null ? true : x.EndDate >= endDate)
            .ToListAsync();
        return cimps.OrderBy(x => x.Id);
    }

}
