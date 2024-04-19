using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface IExamImplementationService
{
    Task<ExamImplementationDTO?> GetExamImplementationByIdAsync(int id);
    Task<IEnumerable<ExamImplementationDTO>> GetExamImpsByExamIdAsync(int examId);
    Task<IEnumerable<ExamImplementationDTO>?> 
        AddExamImplementationsAndUserExamImplementationsAsync(int userId, string role, IEnumerable<ExamImplementationDTO> examImpDTOs);
    Task<IEnumerable<ExamImplementationDTO>?> DeleteByExamIdAsync(int examId, int userId, string role);
}
