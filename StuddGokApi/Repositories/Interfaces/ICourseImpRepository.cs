using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ICourseImpRepository
{
    Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate, 
        (string role, int userId)? user=null);
    Task<CourseImplementation?> GetCourseImpByIdAsync(int id);
    Task<IEnumerable<int>> GetQualifiedStudentIdsAsync(int courseImpId);
    Task<IEnumerable<User>> GetQualifiedStudentObjectsAsync(int courseImpId);
}
