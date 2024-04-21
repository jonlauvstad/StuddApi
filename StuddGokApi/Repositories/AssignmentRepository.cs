using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;

namespace StuddGokApi.Repositories;

public class AssignmentRepository : RepositoryBase, IAssignmentRepository
{
    private readonly StuddGokDbContext _dbContext;

    public AssignmentRepository(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger, AlertUserList altertUserList) 
        : base(dbContext, logger, altertUserList)
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

    public async Task<Assignment?> AddAssignmentAsync(Assignment assignment, int userId, string role)
    {
        bool isOwner = await IsOwner(userId, role, assignment.Id, assignment.CourseImplementationId);
        if (!isOwner)
        {
            _logger.LogInformation($"User {userId} forsøkte å legge til et arbeidskrav uten tillatelse.");
            
            return null;
        }
        
        EntityEntry<Assignment> e = await _dbContext.Assignments.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
        return e.Entity;
    }

    public async Task<IEnumerable<Assignment>?>
        AddAssignmentAndUserAssignmentImplementationsAsync(List<Assignment> assignmentImps,
            List<List<int>> listOfPartipLists)
    {
             
        bool success = true;

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.Assignments.AddAsync(assignment);
                    await _dbContext.SaveChangesAsync();

                    
    }

                



    public async Task<bool> IsOwner(int userId, string role, int assignmentId, int? courseImplementationId = null)
    {
        return await IsOwnerOf(userId, role, assignmentId, GetCourseImpId_FromObjectById, courseImplementationId);
    }

    public async Task<Assignment?> GetAssignmentAsync(int id)
    {
        return await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
       
    }

    public async Task<int?> GetCourseImpId_FromObjectById(int assignmentId)
    {
        Assignment? assignment = await GetAssignmentAsync(assignmentId);
        if (assignment == null) return null;
        return assignment.Id;
    }
}


