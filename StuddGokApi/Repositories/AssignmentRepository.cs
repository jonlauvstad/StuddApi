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





    public async Task<Assignment?> GetAssignmentByIdAsync(int id)
    {
        return await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync(int? courseImpId, int? userId, string role)
    {

        // admin should be able to fetch all
        // teachers : IsOwner check
        // students : IsEnrolled? (check courseImpId)

        IEnumerable<Assignment> assignments;
        if (courseImpId != null)
        {
            assignments = await _dbContext.Assignments.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        }
        else
        {
            assignments = await _dbContext.Assignments.ToListAsync();
        }
        if (userId == null)
            return assignments;

        List<Assignment> assList = new List<Assignment>();
        foreach (Assignment a in assignments)
        {
            if (await IsOwner(userId.Value, role, a.Id, a.CourseImplementationId))
            {
                assList.Add(a);
            }
        }
        return assList;   
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





    public async Task<Assignment?> UpdateAssignmentAsync(int id, Assignment assignment, int userId, string role)
    {
        Assignment? a = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (a == null)
        {
            _logger.LogInformation($"Ingen arbeidskrav med id {id} funnet.");
            return null;
        }



        bool isOwner = await IsOwner(userId, role, assignment.Id, assignment.CourseImplementationId);
        if (!isOwner)
        {
            _logger.LogInformation($"User {userId} forsøkte å endre et arbeidskrav uten tillatelse.");
            return null;
        }

        a.CourseImplementationId = assignment.CourseImplementationId;
        a.Name = assignment.Name;
        a.Deadline = assignment.Deadline;
        a.Description = assignment.Description;
        a.Mandatory = assignment.Mandatory;

        await _dbContext.SaveChangesAsync();
        return a;


        // TODO: Add check if assignment is not in a course implementation that is not active

    }

    public async Task<Assignment?> DeleteAssignmentAsync(int id, int userId, string role)
    {
        Assignment? a = await GetAssignmentByIdAsync(id);
        if ( a == null)
        {
            _logger.LogInformation($"Ingen arbeidskrav med id {id} funnet.");
            return null;
        }


        int deletedAssignment = await _dbContext.Assignments.Where(x => x.Id == id).ExecuteDeleteAsync();
        // await _dbContext.SaveChangesAsync();

        if (deletedAssignment == 0)
        {
            _logger.LogInformation($"Ingen arbeidskrav med id {id} ble slettet.");
            return null;
        }
        return a;
    }

    // -------------------------------------------------------------------------

    public async Task<bool> IsOwner(int userId, string role, int assignmentId, int? courseImplementationId = null)
    {
        return await IsOwnerOf(userId, role, assignmentId, GetCourseImpId_FromObjectById, courseImplementationId);
    }


    public async Task<int?> GetCourseImpId_FromObjectById(int assignmentId)
    {
        Assignment? assignment = await GetAssignmentByIdAsync(assignmentId);
        if (assignment == null) return null;
        return assignment.CourseImplementationId;
    }


}





//public async Task<IEnumerable<Assignment>?>
//    AddAssignmentAndUserAssignmentImplementationsAsync(List<Assignment> assignmentImps,
//        List<List<int>> listOfPartipLists)
//{

//    bool success = true;

//    var strategy = _dbContext.Database.CreateExecutionStrategy();

//    await strategy.ExecuteAsync(async () =>
//    {
//        using (var transaction = _dbContext.Database.BeginTransaction())
//        {
//            try
//            {
//                await _dbContext.Assignments.AddAsync(assignment);
//                await _dbContext.SaveChangesAsync();
//            }
//            catch
//            {
//                success = false;
//                throw;
//            }
//        }
//    });
//    return assignmentImps;


//}



    //public async Task<Assignment?> GetAssignmentById(int id)
    //{
    //    IEnumerable<Assignment> assignments = await _dbContext.Assignments.Where(a => a.Id == id).ToListAsync(); 
    //    Assignment? assignment = assignments.FirstOrDefault();

    //    return assignment;
    //}