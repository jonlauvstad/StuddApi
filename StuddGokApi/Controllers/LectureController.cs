﻿using Microsoft.AspNetCore.Authorization;
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
    private readonly ILogger<LectureController> _logger;

    public LectureController(ILectureService lectureService, ILogger<LectureController> logger)
    {
        _lectureService = lectureService;
        _logger = logger;
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
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        return await _lectureService.AddLectureAsync(lectureDTO, user_id, role);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("{id}", Name = "DeleteLectureById")]
    public async Task<ActionResult<LectureDTO>> DeleteLectureById([FromRoute] int id)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        LectureDTO? lecDTO = await _lectureService.DeleteLectureByIdAsync(id, user_id, role);
        if(lecDTO == null) { return NotFound($"Unable to delete lecture with id {id}"); }
        return Ok(lecDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPut("{id}", Name = "UpdateLecture")]
    public async Task<ActionResult<LectureDTO>> UpdateLecture([FromRoute] int id, [FromBody] LectureDTO lectureDTO)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        LectureDTO? lecDTO = await _lectureService.UpdateLectureAsync(lectureDTO, user_id, role);
        if (lecDTO == null) 
        {
            _logger.LogDebug("Servic'n returnerer null.");
            return NotFound($"Unable to update lecture with id {id}"); 
        }
        return Ok(lecDTO);
    }

    [Authorize]
    [HttpGet(Name = "GetLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> GetLectures(DateTime? startAfter=null, DateTime? endBy=null, int? courseImpId=null,
        int? venueId=null, int? teacherId=null)
    {
        return Ok(await _lectureService.GetLecturesAsync(startAfter, endBy, courseImpId, venueId, teacherId));
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete(Name = "DeleteMultipleLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> DeleteMultiple([FromQuery] string id_string)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;                                                                // NY
        string role = (string)HttpContext.Items["Role"]!;                                                               // NY
        IEnumerable<LectureDTO>? lecDTOs = await _lectureService.DeleteMultipleAsync(id_string, user_id, role);         // var: (id_string)
        if (lecDTOs == null) return NotFound($"Unable to delete all the requested lectures with ids {id_string}.");
        return Ok(lecDTOs);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost("multiple", Name = "AddMultipleLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> AddMultiple([FromBody] IEnumerable<LectureDTO> lectureDTOs)
    {
        _logger.LogDebug(lectureDTOs.ToString());
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;
        IEnumerable<LectureDTO>? lecDTOs = await _lectureService.AddMultipleAsync(lectureDTOs, user_id, role);
        _logger.LogDebug($"lecDTOs is null: {lecDTOs==null}" );
        if (lecDTOs == null) return NotFound("Unable to add the requested lectures.");
        return Ok(lecDTOs);
    }
}
