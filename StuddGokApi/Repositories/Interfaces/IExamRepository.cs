using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IExamRepository
{
    Task<Exam?> AddExamAsync(Exam exam);
    Task<Exam?> UpdateExamAsync(int id, Exam exam);
    Task<Exam?> DeleteExamAsync(int id);
    Task<Exam?> GetExamAsync(int id);
    Task<IEnumerable<Exam>> GetAllExamsAsync(int? courseImplementationId, int? userId, string? role);
    Task<bool> IsOwner(int userId, string role, int lectureId, int? courseImplementationId = null);
    
}
