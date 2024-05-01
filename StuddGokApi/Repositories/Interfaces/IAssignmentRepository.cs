using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IAssignmentRepository
{
    // Task<Assignment?> GetAssignmentById(int id);


    Task<IEnumerable<Assignment?>> GetAllAssignmentsAsync(int? courseImpId, int? userId, string role);

    Task<Assignment?> GetAssignmentByIdAsync(int id);

    Task<Assignment?> AddAssignmentAsync(Assignment assignment, int userId, string role);

    Task<(bool success, string message, Assignment? assignment)> UpdateAssignmentAsync(int id, Assignment updateData, int userId, string role);
    Task<Assignment?> DeleteAssignmentAsync(int id, int userId, string role);








    Task<bool> IsOwner(int userId, string role, int lectureId, int? courseImplementationId = null);

    Task<int?> GetCourseImpId_FromObjectById(int assignmentId);
    

}
