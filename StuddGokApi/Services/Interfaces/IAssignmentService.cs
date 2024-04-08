using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IAssignmentService
{
    Task<AssignmentDTO?> GetAssignmentByIdAsync(int id);

    Task<AssignmentDTO?> AddAssignmentAsync(AssignmentDTO assignment, int userId, string role); 
}
