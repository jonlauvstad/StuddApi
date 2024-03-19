using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
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
        ExamDTO? exam = await _examService.GetExamAsync(id);
        if (exam == null)
        {
            return NotFound($"We could unfortunately not find any exam with id {id}.");
        }
        return Ok(exam);
    }

    [Authorize]
    [HttpPut("{id}", Name = "UpdateExam")]
    public async Task<ActionResult<ExamDTO>> UpdateExam([FromRoute] int id, [FromBody] ExamDTO examDTO)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.UpdateExamAsync(id, examDTO, user_id, role);
        if (exam == null)
        {
            return NotFound($"We could unfortunately not update the exam with id {id}.");
        }
        return Ok(exam);
    }

    [Authorize]
    [HttpDelete("{id}", Name = "DeleteExam")]
    public async Task<ActionResult<ExamDTO>> DeleteExam([FromRoute] int id)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.DeleteExamAsync(id, user_id, role);
        if (exam == null)
        {
            return NotFound($"We could unfortunately not delete the exam with id {id}.");
        }
        return Ok(exam);
    }

    [Authorize]
    [HttpPost(Name = "AddExam")]
    public async Task<ActionResult<ExamDTO>> AddExam([FromBody] ExamDTO examDTO)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        ExamDTO? exam = await _examService.AddExamAsync(examDTO, user_id, role);
        if (exam == null)
        {
            return NotFound($"We could unfortunately not add the exam with to the database.");
        }
        return Ok(exam);
    }

    [Authorize]
    [HttpGet(Name = "GetAllExams")]
    public async Task<ActionResult<IEnumerable<ExamDTO>>> GetAllExams([FromQuery] int? courseImpId=null)
    {
        return Ok(await _examService.GetAllExamsAsync(courseImpId));
    }
}
