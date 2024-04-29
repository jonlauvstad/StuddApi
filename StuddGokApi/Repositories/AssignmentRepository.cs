using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog.Parsing;
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


    private Alert AlertFromAssignment(Assignment assignment, int userId, EntityAction action)
    {

        string actionString = MatchAction(action);
        return new Alert
        {
            UserId = userId,
            Time = DateTime.Now,
            Seen = false,
            Message = $"Arbeidskravet '{assignment.Name}' i {assignment.CourseImplementation!.Name} har blitt {actionString}.",
            Links = $"/Assignment/{assignment.Id}"
        };
    }


    public async Task<Assignment?> GetAssignmentByIdAsync(int id)
    {
        return await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync(int? courseImpId, int? userId, string role)
    {
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
        if (!isOwner) return null;


        EntityEntry<Assignment> entry = _dbContext.Assignments.Add(assignment);
        await _dbContext.SaveChangesAsync(); 

        // Laste inn Assignment på nytt med relatert data
        Assignment? reloadedAssignment = await _dbContext.Assignments
            .Include(a => a.CourseImplementation)
            .FirstOrDefaultAsync(a => a.Id == assignment.Id);


        if (reloadedAssignment == null) return null;

        if (reloadedAssignment.CourseImplementation == null)
        {
            return reloadedAssignment; 
        }

        // Opprette og lagre alert
        Alert alert = AlertFromAssignment(reloadedAssignment, userId, EntityAction.added);
        _dbContext.Alerts.Add(alert);
        await _dbContext.SaveChangesAsync();

        return reloadedAssignment;
    }



    public async Task<Assignment?> UpdateAssignmentAsync(int id, Assignment assignment, int userId, string role)
    {
        Assignment? a = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (a == null) return null;



        bool isOwner = await IsOwner(userId, role, assignment.Id, assignment.CourseImplementationId);
        if (!isOwner) return null;

        a.CourseImplementationId = assignment.CourseImplementationId;
        a.Name = assignment.Name;
        a.Deadline = assignment.Deadline;
        a.Description = assignment.Description;
        a.Mandatory = assignment.Mandatory;

        await _dbContext.SaveChangesAsync();

        // Opprette og lagre alert
        Alert alert = AlertFromAssignment(a, userId, EntityAction.deleted);
        await _dbContext.Alerts.AddAsync(alert);
        await _dbContext.SaveChangesAsync();


        return a;

    }

    public async Task<Assignment?> DeleteAssignmentAsync(int id, int userId, string role)
    {



        Assignment? a = await GetAssignmentByIdAsync(id);
        if ( a == null) return null;


        int deletedAssignment = await _dbContext.Assignments.Where(x => x.Id == id).ExecuteDeleteAsync();
        // await _dbContext.SaveChangesAsync();

        if (deletedAssignment == 0) return null;

        // Opprette og lagre alert
        Alert alert = AlertFromAssignment(a, userId, EntityAction.deleted);
        await _dbContext.Alerts.AddAsync(alert);
        await _dbContext.SaveChangesAsync();

        return a;
    }

    // -------------------------------------------------------------------------

    public async Task<bool> IsOwner(int userId, string role, int assignmentId, int? courseImplementationId = null)
    {
        bool isOwner = await IsOwnerOf(userId, role, assignmentId, GetCourseImpId_FromObjectById, courseImplementationId);
        
        if (!isOwner)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AssignmentRepository", "IsOwner", $"userId:{userId} role:{role} assignmentId:{assignmentId} gives false", System.Diagnostics.Activity.Current?.Id);

        }
        return isOwner;
    }


    public async Task<int?> GetCourseImpId_FromObjectById(int assignmentId)
    {
        Assignment? assignment = await GetAssignmentByIdAsync(assignmentId);
        if (assignment == null) return null;
        return assignment.CourseImplementationId;
    }


}

