using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper<Course, CourseDTO> _courseMapper;

    public CourseService(ICourseRepository courseRepository, IMapper<Course, CourseDTO> courseMapper)
    {
        _courseRepository = courseRepository;
        _courseMapper = courseMapper;
    }

    public async Task<CourseDTO?> GetCourseByIdAsync(int courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);
        return course != null ? _courseMapper.MapToDTO(course) : null;
    }
}

