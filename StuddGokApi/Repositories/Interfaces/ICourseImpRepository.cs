using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ICourseImpRepository
{
    Task<IEnumerable<CourseImplementation>> GetCourseImpsAsync();
}
