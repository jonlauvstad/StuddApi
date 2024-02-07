using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IExamImplementationService
{
    Task<ExamImplementationDTO?> GetExamImplementationByIdAsync(int id);
}
