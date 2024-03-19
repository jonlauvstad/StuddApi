using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IExamService
{
    Task<ExamDTO?> AddExamAsync(ExamDTO examDTO, int userId, string role);
    Task<ExamDTO?> UpdateExamAsync(int id, ExamDTO examDTO, int userId, string role); 
    Task<ExamDTO?> DeleteExamAsync(int id, int userId, string role);
    Task<ExamDTO?> GetExamAsync(int id);
    Task<IEnumerable<ExamDTO>> GetAllExamsAsync(int? courseImplementationId);
}
