using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetAssignmentById")]
    public async Task<ActionResult<AssignmentDTO>> GetAssignmentById([FromRoute] int id)
    {
        AssignmentDTO? assignment = await _assignmentService.GetAssignmentByIdAsync(id);
        if (assignment == null)
        {
            return NotFound("Vi kunne dessverre ikke finne arbeidskravet du leter etter.");
        }
        return Ok(assignment);
    }

    [Authorize]
    [HttpPost(Name = "AddAssignment")]
    public async Task<ActionResult<AssignmentDTO>> AddAssignment([FromBody] AssignmentDTO assignmentDTO)
    {
        AssignmentDTO? assignment = await _assignmentService.AddAssignmentAsync(assignmentDTO);
        if (assignment == null)
        {
            return BadRequest("Vi kunne dessverre ikke legge til nytt arbeidskrav.");
        }
        return Ok(assignment);
    }
}
