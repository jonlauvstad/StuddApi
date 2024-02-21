using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetCourseByIdAsync(int courseId);
}
