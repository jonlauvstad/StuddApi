using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;
using System.Collections.Generic;

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
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamImplementationController", "GetExamImplementationById", $"/ExamImplementation/{id}", "GET", "In", traceId);

        ExamImplementationDTO? eiDTO = await _examImpService.GetExamImplementationByIdAsync(id);
        if (eiDTO == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamImplementationController", "GetExamImplementationById", $"/ExamImplementation/{id}", "GET", "Out", traceId, NotFound().StatusCode);

            return NotFound("Vi kunne dessverre ikke finne eksamenen du leter etter.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamImplementationController", "GetExamImplementationById", $"/ExamImplementation/{id}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(eiDTO);
    }


    [Authorize(Roles = "admin, teacher")]
    [HttpGet("Exam/{examId}", Name = "GetExamImplementationsByExamId")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> GetExamImpsByExamId([FromRoute] int examId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamImplementationController", "GetExamImplementationByExamId", $"/ExamImplementation/Exam/{examId}", "GET", "In", traceId);

        IEnumerable<ExamImplementationDTO> exImps = await _examImpService.GetExamImpsByExamIdAsync(examId);

        _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoExImps:{noVenues}",
            "ExamImplementationController", "GetExamImplementationByExamId", $"/ExamImplementation/Exam/{examId}", "GET", "Out", traceId, 400, exImps.Count() == 0);
        return Ok(exImps);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("Exam/{examId}", Name = "DeleteExamImplementationsByExamId")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> DeleteExamImpsByExamId([FromRoute] int examId)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamImplementationController", "DeleteExamImplementationsByExamId", $"/ExamImplementation/Exam/{examId}", "DELETE", "In", traceId);

        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        IEnumerable<ExamImplementationDTO>? xiDTOs = await _examImpService.DeleteByExamIdAsync(examId, userId, role);

        if (xiDTOs != null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamImplementationController", "DeleteExamImplementationsByExamId", $"/ExamImplementation/Exam/{examId}", "DELETE", "Out", traceId, Ok().StatusCode);
            return Ok(xiDTOs);
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamImplementationController", "DeleteExamImplementationsByExamId", $"/ExamImplementation/Exam/{examId}", "DELETE", "Out", traceId, BadRequest().StatusCode);
        return BadRequest($"Could not delete the examimplementations with examid {examId}");
    }


    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddExamImplementationsAndUserExamImplementations")]
    public async Task<ActionResult<IEnumerable<ExamImplementationDTO>>> 
        AddExamImplementationsAndUserExamImplementations([FromBody] IEnumerable<ExamImplementationDTO> examImpDTOs)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int userId = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "ExamImplementationController", "AddExamImplementationsAndUserExamImplementations", $"/ExamImplementation", "POST", "In", traceId);


        IEnumerable<ExamImplementationDTO>? exImpDTOs = 
            await _examImpService.AddExamImplementationsAndUserExamImplementationsAsync(userId, role, examImpDTOs);
        
        if (exImpDTOs == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamImplementationController", "AddExamImplementationsAndUserExamImplementations", $"/ExamImplementation", "POST", "Out", traceId, BadRequest().StatusCode);

            return BadRequest("Kunne ikke registrere EksamensImplementeringene/BrukerEksamensImplementeringene");
        }

        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamImplementationController", "AddExamImplementationsAndUserExamImplementations", $"/ExamImplementation", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(exImpDTOs);
    }
}
