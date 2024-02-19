using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface ICourseService
{
    Task<CourseDTO> GetCourseByIdAsync(int courseId);
}
