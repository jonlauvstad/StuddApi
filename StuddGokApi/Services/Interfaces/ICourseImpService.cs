using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface ICourseImpService
{
    Task<IEnumerable<CourseImplementationDTO>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate, (string role, int userId)? user = null);
    Task<CourseImplementationDTO?> GetCourseImpByIdAsync(int id);
}
