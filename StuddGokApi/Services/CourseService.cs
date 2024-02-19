using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class CourseService : ICourseService
{
    private readonly StuddGokDbContext _context;
    private readonly CourseMapper _courseMapper;

    public CourseService(StuddGokDbContext context, CourseMapper courseMapper)
    {
        _context = context;
        _courseMapper = courseMapper;
    }

    public async Task<CourseDTO> GetCourseByIdAsync(int courseId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        return course != null ? _courseMapper.MapToDTO(course) : null;
    }
}
