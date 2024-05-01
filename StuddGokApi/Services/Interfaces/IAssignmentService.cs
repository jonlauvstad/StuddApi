using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface IAssignmentService
{

    Task<IEnumerable<AssignmentDTO?>> GetAllAssignmentsAsync(int? courseImpId, int? userId, string role);
    Task<AssignmentDTO?> GetAssignmentByIdAsync(int id);

    Task<AssignmentDTO?> AddAssignmentAsync(AssignmentDTO assignment, int userId, string role);

    Task<(bool success, string message, AssignmentDTO? assignmentDTO)> UpdateAssignmentAsync(int id, AssignmentDTO assignmentDTO, int userId, string role);
    // Task<AssignmentDTO?> UpdateAssignmentAsync(int id, AssignmentDTO assignment, int userId, string role);

    Task<AssignmentDTO?> DeleteAssignmentAsync(int id, int userId, string role);
}
