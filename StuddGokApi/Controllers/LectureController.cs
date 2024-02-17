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
    public async Task<ActionResult<LectureDTO>> GetLectureById([FromRoute] int id)
    {
        LectureDTO? lecture = await _lectureService.GetLectureByIdAsync(id);
        if (lecture == null)
        {
            return NotFound("Vi kunne dessverre ikke finne forelesningen du leter etter.");
        }
        return Ok(lecture);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddLecture")]
    public async Task<ActionResult<LectureBooking>> AddLecture([FromBody] LectureDTO lectureDTO)
    {
        return await _lectureService.AddLectureAsync(lectureDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("{id}", Name = "DeleteLectureById")]
    public async Task<ActionResult<LectureDTO>> DeleteLectureById([FromRoute] int id)
    {
        LectureDTO? lecDTO = await _lectureService.DeleteLectureByIdAsync(id);
        if(lecDTO == null) { return NotFound($"Unable to delete lecture with id {id}"); }
        return Ok(lecDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPut(Name = "UpdateLecture")]
    public async Task<ActionResult<LectureDTO>> UpdateLecture([FromBody] LectureDTO lectureDTO)
    {
        LectureDTO? lecDTO = await _lectureService.UpdateLectureAsync(lectureDTO);
        if (lecDTO == null) { return NotFound($"Unable to update lecture with id {lectureDTO.Id}"); }
        return Ok(lecDTO);
    }


}
