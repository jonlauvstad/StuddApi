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

    public CourseImplementationController(ICourseImpService cimpService)
    {
        _cimpService = cimpService;
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet(Name = "GetCourseImplementations")]
    public async Task<ActionResult<IEnumerable<CourseImplementationDTO>>> 
        GetCourseImps(DateTime? startDate=null, DateTime? endDate=null, string? userRole=null)
    {
        (string role, int userId)? user = userRole == null ? null : 
            (Convert.ToString(HttpContext.Items["Role"]), Convert.ToInt32(HttpContext.Items["UserId"])); 

        IEnumerable<CourseImplementationDTO> ciDTOs = await _cimpService.GetCourseImpsAsync(startDate, endDate, user:user);
        return Ok(ciDTOs);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet("Qualified/{courseImpId}", Name = "GetQualifiedStudentIds")]
    public async Task<ActionResult<IEnumerable<int>>> GetQualifiedStudentIds([FromRoute] int courseImpId)
    {
        return Ok(await _cimpService.GetQualifiedStudentIdsAsync(courseImpId));
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpGet("QualifiedObject/{courseImpId}", Name = "GetQualifiedStudentObjects")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetQualifiedStudentObjects([FromRoute] int courseImpId)
    {
        return Ok(await _cimpService.GetQualifiedStudentObjectsAsync(courseImpId));
    }

    [Authorize]
    [HttpGet("{id}", Name ="GetCourseImpById")]
    public async Task<ActionResult<CourseImplementationDTO>> GetCourseImpById([FromRoute] int id)
    {
        CourseImplementationDTO? ciDTO = await _cimpService.GetCourseImpByIdAsync(id);
        if (ciDTO == null) return NotFound($"Could not find courseimplementation with id {id}");
        return Ok(ciDTO);
    }

}
