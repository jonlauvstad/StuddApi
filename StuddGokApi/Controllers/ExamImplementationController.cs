using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ExamImplementationController : ControllerBase
{
    private readonly IExamImplementationService _examImpService;
    private readonly ILogger<ExamImplementationController> _logger;

    public ExamImplementationController(IExamImplementationService examImpService, ILogger<ExamImplementationController> logger)
    {
        _examImpService = examImpService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetExamImplementationById")]
    public async Task<ActionResult<ExamImplementationDTO>> GetExamImplementationById([FromRoute] int id)
    {
        ExamImplementationDTO? eiDTO = await _examImpService.GetExamImplementationByIdAsync(id);
        if (eiDTO == null)
        {
            return NotFound("Vi kunne dessverre ikke finne eksamenen du leter etter.");
        }
        return Ok(eiDTO);
    }


    [Authorize(Roles = "admin, teacher")]
    [HttpGet("Exam/{examId}", Name = "GetExamImplementationsByExamId")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> GetExamImpsByExamId([FromRoute] int examId)
    {
        return Ok(await _examImpService.GetExamImpsByExamIdAsync(examId));
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("Exam/{examId}", Name = "DeleteExamImplementationsByExamId")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> DeleteExamImpsByExamId([FromRoute] int examId)
    {
        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<ExamImplementationDTO>? xiDTOs = await _examImpService.DeleteByExamIdAsync(examId, userId, role);
        if (xiDTOs != null) return Ok(xiDTOs);
        return BadRequest($"Could not delete the examimplementations with examid {examId}");
    }


    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddExamImplementationsAndUserExamImplementations")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> 
        AddExamImplementationsAndUserExamImplementations([FromBody] IEnumerable<ExamImplementationDTO> examImpDTOs)
    {
        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<ExamImplementationDTO>? exImpDTOs = 
            await _examImpService.AddExamImplementationsAndUserExamImplementationsAsync(userId, role, examImpDTOs);
        
        if (exImpDTOs == null)
        {
            return BadRequest("Kunne ikke registrere EksamensImplementeringene/BrukerEksamensImplementeringene");
        }
        return Ok(exImpDTOs);
    }
}
