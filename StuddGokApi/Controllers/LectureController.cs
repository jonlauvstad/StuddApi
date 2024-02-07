using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LectureController : ControllerBase
{
    private readonly ILectureService _lectureService;

    public LectureController(ILectureService lectureService)
    {
        _lectureService = lectureService;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetLectureById")]
    public async Task<ActionResult<LectureDTO>> GetAssignmentById([FromRoute] int id)
    {
        LectureDTO? lecture = await _lectureService.GetLectureByIdAsync(id);
        if (lecture == null)
        {
            return NotFound("Vi kunne dessverre ikke finne forelesningen du leter etter.");
        }
        return Ok(lecture);
    }
}
