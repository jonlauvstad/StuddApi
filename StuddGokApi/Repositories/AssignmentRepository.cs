using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;

namespace StuddGokApi.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly StuddGokDbContext _dbContext;

    public AssignmentRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<Assignment?> GetAssignmentById(int id)
    {
        IEnumerable<Assignment> assignments = await _dbContext.Assignments.Where(a => a.Id == id).ToListAsync(); 
        Assignment? assignment = assignments.FirstOrDefault();
        //if (assignment != null)
        //{
        //    IEnumerable<CourseImplementation> cis = await _dbContext.CourseImplementations.Where(x => x.Id == assignment.CourseImplementationId).ToListAsync();
        //    assignment.CourseImplementation = cis.FirstOrDefault();
        //}
        return assignment;
    }

    public async Task<Assignment?> AddAssignmentAsync(Assignment assignment)
    {
        EntityEntry<Assignment> e = await _dbContext.Assignments.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
        return e.Entity;

    }

}


