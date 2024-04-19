using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IExamImplementationRepository
{
    Task<ExamImplementation?> GetExamImplementationById(int id);
    Task<IEnumerable<ExamImplementation>> GetExamImpsByExamIdAsync(int examId);
    Task<IEnumerable<ExamImplementation>?> 
        AddExamImplementationsAndUserExamImplementationsAsync(List<ExamImplementation> examImps, List<List<int>> listOfPartipLists);
    Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null);
    Task<bool> ValidTime_ProgCourses_ExamImpAsync(int examId, IEnumerable<ExamImplementationDTO> exImpDTOs);
    Task<bool> DeleteByExamIdAsync(int examId);
}
