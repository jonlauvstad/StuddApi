using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface ICourseImpService
{
    Task<IEnumerable<CourseImplementationDTO>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate, (string role, int userId)? user = null);
    Task<CourseImplementationDTO?> GetCourseImpByIdAsync(int id);
    Task<IEnumerable<int>> GetQualifiedStudentIdsAsync(int courseImpId);
    Task<IEnumerable<UserDTO>> GetQualifiedStudentObjectsAsync(int courseImpId);
}
