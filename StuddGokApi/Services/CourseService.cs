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
    private readonly ILogger<CourseService> _logger;

    public CourseService(ICourseRepository courseRepository, IMapper<Course, CourseDTO> courseMapper, ILogger<CourseService> logger)
    {
        _courseRepository = courseRepository;
        _courseMapper = courseMapper;
        _logger = logger;
    }

    public async Task<CourseDTO?> GetCourseByIdAsync(int courseId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        var course = await _courseRepository.GetCourseByIdAsync(courseId);

        if (course == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
            "CourseService", "GetCourseByIdAsync", "_courseRepository.GetCourseByIdAsync returns null", traceId);

            return null;

        }

        return _courseMapper.MapToDTO(course);
    }
}

