using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IAssignmentRepository
{
    Task<Assignment?> GetAssignmentById(int id);

    Task<Assignment?> AddAssignmentAsync(Assignment assignment, int userId, string role);

    Task<bool> IsOwner(int userId, string role, int lectureId, int? courseImplementationId = null);

    Task<Assignment?> GetAssignmentAsync(int id);

    Task<int?> GetCourseImpId_FromObjectById(int assignmentId);
    
    Task<IEnumerable<Assignment>?>
        AddAssignmentAndUserAssignmentImplementationsAsync(List<Assignment> assignmentImps,
            List<List<int>> 
}
