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

    public async Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync()
    {
        IEnumerable<CourseImplementation> cimps = await _dbContext.CourseImplementations.ToListAsync();
        return cimps.OrderBy(x => x.Id);
    }
}
