using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Extensions;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamService _examService;
    private readonly ILogger<ExamController> _logger;

    public ExamController(IExamService examService, ILogger<ExamController> logger)
    {
        _examService = examService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetExam")]
    public async Task<ActionResult<ExamDTO>> GetExam([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "ExamController", "GetExam", $"/Exam/{id}", "GET", "In", traceId);
        
        ExamDTO? exam = await _examService.GetExamAsync(id);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamController", "GetExam", $"/Exam/{id}", "GET", "Out", traceId, NotFound().StatusCode);
            
            return NotFound($"We could unfortunately not find any exam with id {id}.");
        }
        
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamController", "GetExam", $"/Exam/{id}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(exam);
    }

    [Authorize]
    [HttpPut("{id}", Name = "UpdateExam")]
    public async Task<ActionResult<ExamDTO>> UpdateExam([FromRoute] int id, [FromBody] ExamDTO examDTO)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "ExamController", "UpdateExam", $"/Exam/{id}", "PUT", "In", traceId);
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.UpdateExamAsync(id, examDTO, user_id, role);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamController", "UpdateExam", $"/Exam/{id}", "PUT", "Out", traceId, NotFound().StatusCode);
            return NotFound($"We could unfortunately not update the exam with id {id}.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamController", "UpdateExam", $"/Exam/{id}", "PUT", "Out", traceId, Ok().StatusCode);
        return Ok(exam);
    }

    [Authorize]
    [HttpDelete("{id}", Name = "DeleteExam")]
    public async Task<ActionResult<ExamDTO>> DeleteExam([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "ExamController", "DeleteExam", $"/Exam/{id}", "DELETE", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.DeleteExamAsync(id, user_id, role);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamController", "DeleteExam", $"/Exam/{id}", "DELETE", "Out", traceId, NotFound().StatusCode);
            return NotFound($"We could unfortunately not delete the exam with id {id}.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamController", "DeleteExam", $"/Exam/{id}", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(exam);
    }

    [Authorize]
    [HttpPost(Name = "AddExam")]
    public async Task<ActionResult<ExamDTO>> AddExam([FromBody] ExamDTO examDTO)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "ExamController", "AddExam", $"/Exam", "POST", "In", traceId);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.AddExamAsync(examDTO, user_id, role);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamController", "AddExam", $"/Exam", "POST", "Out", traceId, NotFound().StatusCode);
            return NotFound($"We could unfortunately not add the exam to the database.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamController", "AddExam", $"/Exam", "POST", "Out", traceId, 400);
        return Ok(exam);
    }

    [Authorize]
    [HttpGet(Name = "GetAllExams")]
    public async Task<ActionResult<IEnumerable<ExamDTO>>> GetAllExams([FromQuery] int? courseImpId=null, [FromQuery] bool isOwner=false)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
            "ExamController", "GetAllExams", $"/Exam", "GET", "In", traceId);

        if (isOwner)
        {
            int user_id = (int)HttpContext.Items["UserId"]!;
            string role = (string)HttpContext.Items["Role"]!;

            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "ExamController", "GetAllExams", $"/Exam", "GET", "Out", traceId, Ok().StatusCode);

            return Ok(await _examService.GetAllExamsAsync(courseImpId, userId:user_id, role:role));
        }
        
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "ExamController", "GetAllExams", $"/Exam", "GET", "Out", traceId, Ok().StatusCode);  
        return Ok(await _examService.GetAllExamsAsync(courseImpId, null, null));
    }
}
