using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CourseImplementationController : ControllerBase
{
    private readonly ICourseImpService _cimpService;
    private readonly ILogger<CourseImplementationController> _logger;

    public CourseImplementationController(ICourseImpService cimpService, ILogger<CourseImplementationController> logger)
    {
        _cimpService = cimpService;
        _logger = logger;
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet(Name = "GetCourseImplementations")]
    public async Task<ActionResult<IEnumerable<CourseImplementationDTO>>> 
        GetCourseImps(DateTime? startDate=null, DateTime? endDate=null, string? userRole=null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetCourseImps", "/CourseImplementation", "GET", "In", traceId);


        (string role, int userId)? user = userRole == null ? null : 
            (Convert.ToString(HttpContext.Items["Role"]), Convert.ToInt32(HttpContext.Items["UserId"])); 
        IEnumerable<CourseImplementationDTO> ciDTOs = await _cimpService.GetCourseImpsAsync(startDate, endDate, user:user);

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetCourseImps", "/CourseImplementation", "GET", "Out", traceId);

        return Ok(ciDTOs);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet("Qualified/{courseImpId}", Name = "GetQualifiedStudentIds")]
    public async Task<ActionResult<IEnumerable<int>>> GetQualifiedStudentIds([FromRoute] int courseImpId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetQualifiedStudentIds", "/CourseImplementation", "GET", "In", traceId);


        return Ok(await _cimpService.GetQualifiedStudentIdsAsync(courseImpId));
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet("QualifiedObject/{courseImpId}", Name = "GetQualifiedStudentObjects")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetQualifiedStudentObjects([FromRoute] int courseImpId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetQualifiedStudentObjects", "/CourseImplementation", "GET", "In", traceId);


        return Ok(await _cimpService.GetQualifiedStudentObjectsAsync(courseImpId));
    }

    [Authorize]
    [HttpGet("{id}", Name ="GetCourseImpById")]
    public async Task<ActionResult<CourseImplementationDTO>> GetCourseImpById([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetCourseImpById", $"/CourseImplementation/{id}", "GET", "In", traceId);

        CourseImplementationDTO? ciDTO = await _cimpService.GetCourseImpByIdAsync(id);
        if (ciDTO == null) return NotFound($"Could not find courseimplementation with id {id}");

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "CourseImplementationController", "GetCourseImpById", $"/CourseImplementation/{id}", "GET", "Out", traceId);
        return Ok(ciDTO);
    }

}
