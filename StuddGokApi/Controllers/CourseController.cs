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
        CourseDTO? courseDTO = await _courseService.GetCourseByIdAsync(id);
        if (courseDTO == null) return NotFound($"Could not find courseimplementation with id {id}");
        return Ok(courseDTO);
    }
}
