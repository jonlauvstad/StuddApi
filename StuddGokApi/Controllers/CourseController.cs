using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CourseController> _logger;

    public CourseController(ICourseService courseService, ILogger<CourseController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetCourseById")]
    public async Task<ActionResult<CourseDTO>> GetCourseImpById([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseController", "GetCourseById", $"/Course/{id}", "GET", "In", traceId);


        CourseDTO? courseDTO = await _courseService.GetCourseByIdAsync(id);
        if (courseDTO == null) return NotFound($"Could not find courseimplementation with id {id}");

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseController", "GetCourseById", $"/Course/{id}", "GET", "Out", traceId);

        return Ok(courseDTO);
    }
}
