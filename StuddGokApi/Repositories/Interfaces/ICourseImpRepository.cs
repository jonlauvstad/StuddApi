using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ICourseImpRepository
{
    Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate);
    Task<CourseImplementation?> GetCourseImpByIdAsync(int id);
}
