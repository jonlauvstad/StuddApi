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
        GetCourseImps(DateTime? startDate=null, DateTime? endDate=null)
    {
        IEnumerable<CourseImplementationDTO> ciDTOs = await _cimpService.GetCourseImpsAsync(startDate, endDate);
        return Ok(ciDTOs);
    }
}
