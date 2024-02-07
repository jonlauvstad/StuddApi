using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IExamImplementationRepository
{
    Task<ExamImplementation?> GetExamImplementationById(int id);
}
