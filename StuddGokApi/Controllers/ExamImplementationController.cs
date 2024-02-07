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

    public ExamImplementationController(IExamImplementationService examImpService)
    {
        _examImpService = examImpService;
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
}
