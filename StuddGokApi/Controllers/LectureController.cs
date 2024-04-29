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
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "GetLectureById", $"/Lecture/{id}", "GET", "In", traceId);

        LectureDTO? lecture = await _lectureService.GetLectureByIdAsync(id);
        if (lecture == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "GetLectureById", $"/Lecture/{id}", "GET", "Out", traceId, NotFound().StatusCode);
            return NotFound("Vi kunne dessverre ikke finne forelesningen du leter etter.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "GetLectureById", $"/Lecture/{id}", "GET", "Out", traceId, Ok().StatusCode);
        return Ok(lecture);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost(Name = "AddLecture")]
    public async Task<ActionResult<LectureBooking>> AddLecture([FromBody] LectureDTO lectureDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "AddLecture", $"/Lecture", "POST", "In", traceId);
        
        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        LectureBooking? lectureBooking = await  _lectureService.AddLectureAsync(lectureDTO, user_id, role);
        if (lectureBooking == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "AddLecture", $"/Lecture", "POST", "Out", traceId, BadRequest().StatusCode);
            return BadRequest("Kunne ikke registrere forelesningen.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LectureController", "AddLecture", $"/Lecture", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(lectureBooking);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete("{id}", Name = "DeleteLectureById")]
    public async Task<ActionResult<LectureDTO>> DeleteLectureById([FromRoute] int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "DeleteLectureById", $"/Lecture/{id}", "DELETE", "In", traceId);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        LectureDTO? lecDTO = await _lectureService.DeleteLectureByIdAsync(id, user_id, role);
        if(lecDTO == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "DeleteLectureById", $"/Lecture/{id}", "DELETE", "Out", traceId, NotFound().StatusCode);
            return NotFound($"Unable to delete lecture with id {id}"); 
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LectureController", "DeleteLectureById", $"/Lecture/{id}", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(lecDTO);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPut("{id}", Name = "UpdateLecture")]
    public async Task<ActionResult<LectureDTO>> UpdateLecture([FromRoute] int id, [FromBody] LectureDTO lectureDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "UpdateLecture", $"/Lecture/{id}", "PUT", "In", traceId);

        LectureDTO? lecDTO = await _lectureService.UpdateLectureAsync(lectureDTO, user_id, role);
        if (lecDTO == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "UpdateLecture", $"/Lecture/{id}", "PUT", "Out", traceId, NotFound().StatusCode);
            return NotFound($"Unable to update lecture with id {id}"); 
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LectureController", "UpdateLecture", $"/Lecture/{id}", "PUT", "Out", traceId, Ok().StatusCode);
        return Ok(lecDTO);
    }

    [Authorize]
    [HttpGet(Name = "GetLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> GetLectures(DateTime? startAfter=null, DateTime? endBy=null, int? courseImpId=null,
        int? venueId=null, int? teacherId=null)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "GetLectures", $"/Lecture", "GET", "In", traceId);

        IEnumerable<LectureDTO> lectures = await _lectureService.GetLecturesAsync(startAfter, endBy, courseImpId, venueId, teacherId);

        _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoLectures:{noVenues}",
            "LectureController", "GetLectures", $"/Lecture", "GET", "Out", traceId, 400, lectures.Count() == 0);
        
        return Ok(lectures);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpDelete(Name = "DeleteMultipleLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> DeleteMultiple([FromQuery] string id_string)
    {
        int user_id = (int)HttpContext.Items["UserId"]!;                                                                
        string role = (string)HttpContext.Items["Role"]!;

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "DeleteMultipleLectures", $"/Lecture", "DELETE", "In", traceId);

        IEnumerable<LectureDTO>? lecDTOs = await _lectureService.DeleteMultipleAsync(id_string, user_id, role);         
        
        if (lecDTOs == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "DeleteMultipleLectures", $"/Lecture", "DELETE", "Out", traceId, NotFound().StatusCode);
            return NotFound($"Unable to delete all the requested lectures with ids {id_string}.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LectureController", "DeleteMultipleLectures", $"/Lecture", "DELETE", "Out", traceId, Ok().StatusCode);
        return Ok(lecDTOs);
    }

    [Authorize(Roles = "admin, teacher")]
    [HttpPost("multiple", Name = "AddMultipleLectures")]
    public async Task<ActionResult<IEnumerable<LectureDTO>>> AddMultiple([FromBody] IEnumerable<LectureDTO> lectureDTOs)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int user_id = (int)HttpContext.Items["UserId"]!;
        string role = (string)HttpContext.Items["Role"]!;

        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                "LectureController", "AddMultipleLectures", $"/Lecture/multiple", "POST", "In", traceId);

        IEnumerable<LectureDTO>? lecDTOs = await _lectureService.AddMultipleAsync(lectureDTOs, user_id, role);
        if (lecDTOs == null) 
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
                "LectureController", "AddMultipleLectures", $"/Lecture/multiple", "POST", "Out", traceId, NotFound().StatusCode);
            return NotFound("Unable to add the requested lectures.");
        }
        _logger.LogDebug("Class:{class}, Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}",
            "LectureController", "AddMultipleLectures", $"/Lecture/multiple", "POST", "Out", traceId, Ok().StatusCode);
        return Ok(lecDTOs);
    }
}
