using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;
    private readonly ILogger<AssignmentController> _logger;

    public AssignmentController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [Authorize]
    [HttpGet(Name = "GetAllAssignments")]
    public async Task<ActionResult<AssignmentDTO>> GetAllAssignments([FromQuery] int? courseImpId = null,
                                                                 [FromQuery] bool isOwner = false)
    {
        if (isOwner)
        {
            int user_id = (int)HttpContext.Items["UserId"]!;
            string role = (string)HttpContext.Items["Role"]!;
            return Ok(await _assignmentService.GetAllAssignmentsAsync(courseImpId, userId: user_id, role: role));
        }

        return Ok(await _assignmentService.GetAllAssignmentsAsync(courseImpId, null, null));
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
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        AssignmentDTO? assignment = await _assignmentService.AddAssignmentAsync(assignmentDTO, user_id, role);
        if (assignment == null)
        {
            return BadRequest("Vi kunne dessverre ikke legge til nytt arbeidskrav.");
        }
        return Ok(assignment);
    }

    [Authorize]
    [HttpPut("{id}", Name = "UpdateAssignment")]
    public async Task<ActionResult<AssignmentDTO>> UpdateAssignment([FromRoute] int id, 
                                                                    [FromBody] AssignmentDTO assignmentDTO)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        AssignmentDTO? assignment = await _assignmentService.UpdateAssignmentAsync(id, assignmentDTO, user_id, role);
        if ( assignment == null)
        {
            return NotFound($"Kunne ikke finne arbeidskravet med id {id}");
        }
        return Ok(assignment);
    }

    [Authorize]
    [HttpDelete("{id}", Name = "DeleteAssignment")]
    public async Task<ActionResult<AssignmentDTO>> DeleteAssignment([FromRoute] int id)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        AssignmentDTO? assignment = await _assignmentService.DeleteAssignmentAsync(id, user_id, role);

        if (assignment == null)
        {
            
            return NotFound($"We could unfortunately not delete the exam with id {id}.");
        }
        return Ok(assignment);
    }




}
