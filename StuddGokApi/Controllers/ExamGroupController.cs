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
        return Ok(await _examGroupService.GetExamGroupsAsync(examId, userId, name));
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost("Exam/{examId}", Name = "AddExamGroup")]
    public async Task<ActionResult<IEnumerable<ExamGroupDTO>>> AddExamGroup([FromRoute] int examId, [FromBody] IEnumerable<ExamGroupDTO> exGrDTOs)
    {
        //_logger.LogDebug(exGrDTOs.Count().ToString());
        //return Ok(exGrDTOs.Count());

        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<ExamGroupDTO>? examGroups = await _examGroupService.AddExamGroupAsync(userId, role, examId, exGrDTOs);
        if (examGroups == null) return BadRequest("Kunne ikke registrere gruppen i databasen. Er navnet unikt?");
        return Ok(examGroups);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("{id}", Name = "RemoveExamGroupEntry")]
    public async Task<ActionResult<ExamGroupDTO>> RemoveExamGroupEntry([FromRoute] int id)
    {
        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamGroupDTO? examGroupDTO = await _examGroupService.RemoveExamGroupEntryAsync(userId, role, id);
        if (examGroupDTO == null) return BadRequest("Kunne ikke fjerne gruppeoppføringen.");
        return Ok(examGroupDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddOneExamGroup")]
    public async Task<ActionResult<ExamGroupDTO>> AddOneExamGroup([FromBody] ExamGroupDTO examGroup)
    {
        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamGroupDTO? examGroupDTO = await _examGroupService.AddOneExamGroupAsync(userId, role, examGroup);
        if (examGroupDTO == null) return BadRequest("Kunne ikke registrere gruppeoppføringen.");
        return Ok(examGroupDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete(Name = "DeleteExamGroupsByExamIdAndName")]
    public async Task<ActionResult<Dictionary<string, int>>> DeleteExamGroupsByExamIdAndName(int examId, string name)
    {
        int numDeleted = await _examGroupService.DeleteExamGroupsByExamIdAndNameAsync(examId, name);
        if (numDeleted == 0) return BadRequest("Kunne ikke slette gruppen/gruppeoppføringene");
        Dictionary<string, int> returnDict = new Dictionary<string, int>() { { "numDeleted", numDeleted} };
        return Ok(returnDict);
    }
}
