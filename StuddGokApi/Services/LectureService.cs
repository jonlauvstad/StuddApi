using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using System.Data;

namespace StuddGokApi.Services;

public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IVenueRepository _venueRepository;
    private readonly ICourseImpService _courseImpService;
    private readonly IMapper<Lecture, LectureDTO> _lectureMapper;
    private readonly IMapper<Event, EventDTO> _eventMapper;
    private readonly IMapper<LectureVenue, LectureVenueDTO> _lectureVenueMapper;
    private readonly ILogger<LectureService> _logger;

    public LectureService(ILectureRepository lectureRepository, IVenueRepository venueRepository, ICourseImpService courseImpService,
        IMapper<Lecture, LectureDTO> lectureMapper, IMapper<Event, EventDTO> eventMapper, 
        IMapper<LectureVenue, LectureVenueDTO> lectureVenueMapper, ILogger<LectureService> logger)
    {
        _lectureRepository = lectureRepository;
        _lectureMapper = lectureMapper;
        _venueRepository = venueRepository;
        _eventMapper = eventMapper;
        _courseImpService = courseImpService;
        _lectureVenueMapper = lectureVenueMapper;
        _logger = logger;
    }

    public async Task<LectureDTO?> UpdateLectureAsync(LectureDTO lecture, int userId, string role)
    {
        // SJEKK PÅ TEACHER ER 'EIER'
        if (! await _lectureRepository.IsOwner(userId, role, lecture.Id, courseImplementationId:lecture.CourseImplementationId)) return null;

        string? validated = await ValidateDates(lecture);
        if (validated != null) { return null; }

        // Check if venue is available - that is if venues selected...
        int venueId = 0;
        if (lecture.VenueIds.Any())
        {
            venueId = lecture.VenueIds.FirstOrDefault();
            Event? e = await _venueRepository.CheckVenueAsync(venueId, lecture.StartTime, lecture.EndTime);
            if (e != null) 
            { 
                if(e.UnderlyingId != lecture.Id || e.TypeEng != "Lecture") { return null; } 
            }
        }

        // Check wheteher teacher is available
        Lecture? teacherLecture = await _lectureRepository.CheckTeacher(lecture.CourseImplementationId, lecture.StartTime, lecture.EndTime);
        if (teacherLecture != null)
        {
            if(teacherLecture.Id != lecture.Id) { return null; }
        }

        // Update lecture (and venue - that is lLectureVenue)
        Lecture? returnLecture = await _lectureRepository.UpdateLectureAndVenueAsync(_lectureMapper.MapToModel(lecture), venueId);
        if (returnLecture == null) return null;
        LectureDTO returnLectureDTO = _lectureMapper.MapToDTO(returnLecture);
        return await AddTeachers(returnLectureDTO); //_lectureMapper.MapToDTO(returnLecture);
    }

    public async Task<LectureBooking> AddLectureAsync(LectureDTO lecture, int userId, string role)
    {
        // SJEKK PÅ TEACHER ER 'EIER'
        if (!await _lectureRepository.IsOwner(userId, role, lecture.Id, courseImplementationId: lecture.CourseImplementationId))
        {
            return new LectureBooking(null, null, null, null, null, failMsg: $"Not authorized to add lecture to course with id {lecture.CourseImplementationId}");
        }

        string? validated = await ValidateDates(lecture);
        if (validated != null) 
        {
            return new LectureBooking(null, null, null, null, null, failMsg:validated);
        }

        EventDTO? venueEvent = null;
        CourseImplementationDTO? ciDTO = await _courseImpService.GetCourseImpByIdAsync(lecture.CourseImplementationId);
        int venueId = 0;

        // Check if venue is available - that is if venues selected...
        if (lecture.VenueIds.Any())
        {
            venueId = lecture.VenueIds.FirstOrDefault();
            Event? e = await _venueRepository.CheckVenueAsync(venueId, lecture.StartTime, lecture.EndTime);
            if (e != null) { venueEvent = _eventMapper.MapToDTO(e); }
        }
        
        // Check wheteher teacher is available
        Lecture? teacherLecture = await _lectureRepository.CheckTeacher(lecture.CourseImplementationId, lecture.StartTime, lecture.EndTime);

        // Return if venue or teacher not available
        if (venueEvent != null || teacherLecture != null) 
        {
            return new LectureBooking(null,
                venueEvent,
                teacherLecture == null ? null : _lectureMapper.MapToDTO(teacherLecture),
                null,
                ciDTO);
        }

        
        if (venueId == 0)
        {
            Lecture? returnLecture = await _lectureRepository.AddLectureAsync(_lectureMapper.MapToModel(lecture));
            return new LectureBooking(returnLecture==null ? null : _lectureMapper.MapToDTO(returnLecture),
                venueEvent,
                teacherLecture == null ? null : _lectureMapper.MapToDTO(teacherLecture),
                null,       //Venue
                ciDTO);     //
        }

        (Lecture?, LectureVenue?)  lectureLectureVenue = 
            await _lectureRepository.AddLectureAndVenueAsync(_lectureMapper.MapToModel(lecture), venueId);

        Venue? venue = await _venueRepository.GetVenueByIdAsync(venueId);

        return new LectureBooking(lectureLectureVenue.Item1 != null ? _lectureMapper.MapToDTO(lectureLectureVenue.Item1) : null,
                venueEvent,
                teacherLecture == null ? null : _lectureMapper.MapToDTO(teacherLecture),
                venue,                       
                ciDTO);
    }

    public async Task<LectureDTO?> DeleteLectureByIdAsync(int id, int userId, string role)
    {
        // SJEKK PÅ TEACHER ER 'EIER'
        if (!await _lectureRepository.IsOwner(userId, role, id, courseImplementationId: null)) return null;

        Lecture? lecture = await _lectureRepository.DeleteLectureByIdAsync(id);
        if (lecture == null) { return null; }
        LectureDTO lecDTO = _lectureMapper.MapToDTO(lecture);
        await AddTeachers(lecDTO);
        return lecDTO;
    }

    public async Task<LectureDTO?> GetLectureByIdAsync(int id)
    {
        Lecture? lecture = await _lectureRepository.GetLectureById(id);
        if (lecture == null) { return null; }
        LectureDTO lecDTO = _lectureMapper.MapToDTO(lecture);
        await AddTeachers(lecDTO);
        return lecDTO; 
    }

    public async Task<IEnumerable<LectureDTO>> GetLecturesAsync(DateTime? startAfter, DateTime? endBy, int? courseImpId, int? venueId, int? teacherId)
    {
        IEnumerable<Lecture> lectures = await _lectureRepository.GetLecturesAsync(startAfter, endBy, courseImpId, venueId, teacherId);
        IEnumerable<LectureDTO> lecDTOs = from lec in lectures select _lectureMapper.MapToDTO(lec);  
        
        List<LectureDTO> dTOs = new List<LectureDTO>();
        foreach (var dto in lecDTOs)
        {
            dTOs.Add(await AddTeachers(dto));
        }

        return dTOs;
    }

    public async Task<IEnumerable<LectureDTO>?> DeleteMultipleAsync(string id_string)
    {
        IEnumerable<int> ids = from str in id_string.Split(",") select Convert.ToInt32(str);
        IEnumerable<Lecture>? lectures = await _lectureRepository.DeleteMultipleAsync(ids);
        if (lectures == null) return null;
        return from lec in lectures select _lectureMapper.MapToDTO(lec);
    }

    private async Task<LectureDTO> AddTeachers(LectureDTO lecDTO)
    {
        IEnumerable<User> teachers = await _lectureRepository.GetTeachersByCourseImplementationId(lecDTO.CourseImplementationId);
        IEnumerable<string> teacherNames = from t in teachers select $"{t.FirstName} {t.LastName}";
        lecDTO.TeacherNames = string.Join(", ", teacherNames);
        lecDTO.TeacherUserIds = (from t in teachers select t.Id).ToList();

        IEnumerable<User> progTeachers = await _lectureRepository.GetProgramTeachersByCourseImplementationId(lecDTO.CourseImplementationId);
        lecDTO.ProgramTeacherUserIds = (from pt in progTeachers select pt.Id).ToList();

        return lecDTO;
    }

    private async Task<string?> ValidateDates(LectureDTO lecture)
    {
        string? s = null;

        // fra < til
        if(lecture.StartTime > lecture.EndTime) { return "Starttidspunkt må være etter slutttidspunkt"; }

        // fra > nå
        if(lecture.StartTime <= DateTime.Now) { return "Starttidspunktet må være i fremtiden"; }

        // fra > kursImpStart
        CourseImplementationDTO? ciDTO = await _courseImpService.GetCourseImpByIdAsync(lecture.CourseImplementationId);
        if(lecture.StartTime < ciDTO!.StartDate) { return "Starttidspunktet må være etter at kursetgjennomføringen har startet"; }

        // til < kursSlutt
        if(lecture.EndTime > ciDTO!.EndDate) { return "Slutttidspunktet må være før at kursetgjennomføringen er avsluttet"; }

        return s;
    }

    
}
