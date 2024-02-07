using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IAssignmentRepository
{
    Task<Assignment?> GetAssignmentById(int id);
}
