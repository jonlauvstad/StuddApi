using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ExamGroupController : ControllerBase
{
    private readonly IExamGroupService _examGroupService;
    private readonly ILogger<ExamGroupController> _logger;

    public ExamGroupController(IExamGroupService examGroupService, ILogger<ExamGroupController> logger)
    {
        _examGroupService = examGroupService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet(Name = "GetExamGroups")]
    public async Task<ActionResult<IEnumerable<ExamGroupDTO>>> GetExamGroups([FromQuery] int? examId=null, [FromQuery] int? userId=null, 
                                                                                [FromQuery] string? name = null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamGroupController", "GetExamGroups", $"/ExamGroup", "GET", "In", traceId);

        var groups = await _examGroupService.GetExamGroupsAsync(examId, userId, name);
        _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoGroups:{noVenues}",
            "ExamGroupController", "GetExamGroups", $" /ExamGroup", "GET", "Out", traceId, 400, groups.Count() == 0);
        return Ok(groups);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost("Exam/{examId}", Name = "AddExamGroup")]
    public async Task<ActionResult<IEnumerable<ExamGroupDTO>>> AddExamGroup([FromRoute] int examId, [FromBody] IEnumerable<ExamGroupDTO> exGrDTOs)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamGroupController", "AddExamGroup", $"/ExamGroup/Exam/{examId}", "POST", "In", traceId);

        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<ExamGroupDTO>? examGroups = await _examGroupService.AddExamGroupAsync(userId, role, examId, exGrDTOs);

        if (examGroups == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamGroupController", "AddExamGroup", $"/ExamGroup/Exam/{examId}", "POST", "In", traceId, BadRequest().StatusCode);

            return BadRequest("Kunne ikke registrere gruppen i databasen. Er navnet unikt?");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamGroupController", "AddExamGroup", $"/ExamGroup/Exam/{examId}", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(examGroups);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("{id}", Name = "RemoveExamGroupEntry")]
    public async Task<ActionResult<ExamGroupDTO>> RemoveExamGroupEntry([FromRoute] int id)
    {
        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;


        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamGroupController", "RemoveExamGroupEntry", $"/ExamGroup/{id}", "DELETE", "In", traceId);

        ExamGroupDTO? examGroupDTO = await _examGroupService.RemoveExamGroupEntryAsync(userId, role, id);
        if (examGroupDTO == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamGroupController", "RemoveExamGroupEntry", $"/ExamGroup/{id}", "DELETE", "Out", traceId, BadRequest().StatusCode);

            return BadRequest("Kunne ikke fjerne gruppeoppføringen.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamGroupController", "RemoveExamGroupEntry", $"/ExamGroup/{id}", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(examGroupDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddOneExamGroup")]
    public async Task<ActionResult<ExamGroupDTO>> AddOneExamGroup([FromBody] ExamGroupDTO examGroup)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamGroupController", "AddOneExamGroup", $"/ExamGroup", "POST", "In", traceId);

        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamGroupDTO? examGroupDTO = await _examGroupService.AddOneExamGroupAsync(userId, role, examGroup);
        if (examGroupDTO == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamGroupController", "AddOneExamGroup", $"/ExamGroup", "POST", "Out", traceId, BadRequest().StatusCode);
            return BadRequest("Kunne ikke registrere gruppeoppføringen.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamGroupController", "AddOneExamGroup", $"/ExamGroup", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(examGroupDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete(Name = "DeleteExamGroupsByExamIdAndName")]
    public async Task<ActionResult<Dictionary<string, int>>> DeleteExamGroupsByExamIdAndName(int examId, string name)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamGroupController", "DeleteExamGroupsByExamIdAndName", $"/ExamGroup", "DELETE", "In", traceId);

        int numDeleted = await _examGroupService.DeleteExamGroupsByExamIdAndNameAsync(examId, name);
        if (numDeleted == 0) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamGroupController", "DeleteExamGroupsByExamIdAndName", $"/ExamGroup", "DELETE", "Out", traceId, BadRequest().StatusCode);
            return BadRequest("Kunne ikke slette gruppen/gruppeoppføringene");
        }
        Dictionary<string, int> returnDict = new Dictionary<string, int>() { { "numDeleted", numDeleted} };
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamGroupController", "DeleteExamGroupsByExamIdAndName", $"/ExamGroup", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(returnDict);
    }
}
