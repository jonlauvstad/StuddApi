using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface ICourseImpService
{
    Task<IEnumerable<CourseImplementationDTO>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate);
    Task<CourseImplementationDTO?> GetCourseImpByIdAsync(int id);
}
