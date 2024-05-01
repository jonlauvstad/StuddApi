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

    public AssignmentController(IAssignmentService assignmentService , ILogger<AssignmentController> logger)
    {
        _assignmentService = assignmentService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet(Name = "GetAllAssignments")]
    public async Task<ActionResult<AssignmentDTO>> GetAllAssignments([FromQuery] int? courseImpId = null,
                                                                 [FromQuery] bool isOwner = false)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "AssignmentController", "GetAllAssignments", $"/Assignment", "GET", "In", traceId);

        if (isOwner)
        {
            int user_id = (int)HttpContext.Items["UserId"]!;
            string role = (string)HttpContext.Items["Role"]!;
            return Ok(await _assignmentService.GetAllAssignmentsAsync(courseImpId, userId: user_id, role: role));
        }

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "AssignmentController", "GetAllAssignments", $"/Assignment", "GET", "Out", traceId, Ok().StatusCode);

        return Ok(await _assignmentService.GetAllAssignmentsAsync(courseImpId, null, null));
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetAssignmentById")]
    public async Task<ActionResult<AssignmentDTO>> GetAssignmentById([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "AssignmentController", "GetAssignmentById", $"/Assignment/{id}", "GET", "In", traceId);

        AssignmentDTO? assignment = await _assignmentService.GetAssignmentByIdAsync(id);
        if (assignment == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "AssignmentController", "GetAssignmentById", $"/Assignment/{id}", "GET", "Out", traceId, NotFound().StatusCode);
            return NotFound("Vi kunne dessverre ikke finne arbeidskravet du leter etter.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "AssignmentController", "GetAssignmentById", $"/Assignment/{id}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(assignment);
    }

    [Authorize]
    [HttpPost(Name = "AddAssignment")]
    public async Task<ActionResult<AssignmentDTO>> AddAssignment([FromBody] AssignmentDTO assignmentDTO)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "AssignmentController", "AddAssignment", $"/Assignment", "POST", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        AssignmentDTO? assignment = await _assignmentService.AddAssignmentAsync(assignmentDTO, user_id, role);
        if (assignment == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "AssignmentController", "AddAssignment", $"/Assignment", "POST", "Out", traceId, BadRequest().StatusCode);

            return BadRequest("Vi kunne dessverre ikke legge til nytt arbeidskrav.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "AssignmentController", "AddAssignment", $"/Assignment", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(assignment);
    }

    [Authorize]
    [HttpPut("{id}", Name = "UpdateAssignment")]
    public async Task<IActionResult> UpdateAssignment([FromRoute] int id, [FromBody] AssignmentDTO assignmentDTO)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        var (success, message, updatedAssignment) = await _assignmentService.UpdateAssignmentAsync(id, assignmentDTO, user_id, role);

        if (!success)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, Message:{message}",
                "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "Out", traceId, BadRequest().StatusCode, message);
            return BadRequest(message);
        }

        _logger.LogInformation("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "Out", traceId, Ok().StatusCode);
        return Ok(updatedAssignment);
    }


    //[Authorize]
    //[HttpPut("{id}", Name = "UpdateAssignment")]
    //public async Task<ActionResult<AssignmentDTO>> UpdateAssignment([FromRoute] int id,
    //                                                            [FromBody] AssignmentDTO assignmentDTO)
    //{
    //    string? traceId = System.Diagnostics.Activity.Current?.Id;

    //    _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
    //        "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "In", traceId);

    //    int user_id = (int)HttpContext.Items["UserId"]!;
    //    string role = (string)HttpContext.Items["Role"]!;

    //    AssignmentDTO? assignment = await _assignmentService.UpdateAssignmentAsync(id, assignmentDTO, user_id, role);
    //    if (assignment == null)
    //    {
    //        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
    //            "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "Out", traceId, NotFound().StatusCode);
    //        return NotFound($"Kunne ikke finne arbeidskravet med id {id}");
    //    }
    //    _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
    //        "AssignmentController", "UpdateAssignment", $"/Assignment/{id}", "PUT", "Out", traceId, Ok().StatusCode);
    //    return Ok(assignment);
    //}


    [Authorize]
    [HttpDelete("{id}", Name = "DeleteAssignment")]
    public async Task<ActionResult<AssignmentDTO>> DeleteAssignment([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "AssignmentController", "DeleteAssignment", $"/Assignment/{id}", "DELETE", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        AssignmentDTO? assignment = await _assignmentService.DeleteAssignmentAsync(id, user_id, role);

        if (assignment == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "AssignmentController", "DeleteAssignment", $"/Assignment/{id}", "DELETE", "Out", traceId, NotFound().StatusCode);
            return NotFound($"We could unfortunately not delete the exam with id {id}.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "AssignmentController", "DeleteAssignment", $"/Assignment/{id}", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(assignment);
    }




}
