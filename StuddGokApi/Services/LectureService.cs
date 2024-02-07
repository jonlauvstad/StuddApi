using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IVenueRepository _venueRepository;
    private readonly IMapper<Lecture, LectureDTO> _lectureMapper;
    private readonly IMapper<Event, EventDTO> _eventMapper;

    public LectureService(ILectureRepository lectureRepository, IVenueRepository venueRepository, 
        IMapper<Lecture, LectureDTO> lectureMapper, IMapper<Event, EventDTO> eventMapper)
    {
        _lectureRepository = lectureRepository;
        _lectureMapper = lectureMapper;
        _venueRepository = venueRepository;
        _eventMapper = eventMapper;
    }

    public async Task<(LectureDTO? newLecture, EventDTO? venueEvent, LectureDTO? teacherLecture)> AddLectureAsync(LectureDTO lecture)
    {

        // Check if venue is available - that is if venues selected...
        EventDTO? venueEvent = null;
        if (lecture.VenueIds.Any())
        {
            int venueId = lecture.VenueIds.FirstOrDefault();
            Event? e = await _venueRepository.CheckVenue(venueId, lecture.StartTime, lecture.EndTime);
            if (e != null) { venueEvent = _eventMapper.MapToDTO(e); }
        }
        // Check wheteher teacher is available
        Lecture? teacherLecture = await _lectureRepository.CheckTeacher(lecture.CourseImplementationId, lecture.StartTime, lecture.EndTime);
        // Return if venue or teacher not available
        if (venueEvent != null || teacherLecture != null) 
        { 
            return (null, 
                venueEvent, 
                teacherLecture == null ? null : _lectureMapper.MapToDTO(teacherLecture)); 
        }
        // otherwise, return the new LectureDTO
        Lecture? newLecture = await _lectureRepository.AddLectureAsync(_lectureMapper.MapToModel(lecture));
        return (newLecture != null ? _lectureMapper.MapToDTO(newLecture) : null, 
            null, 
            null );
    }

    public async Task<LectureDTO?> GetLectureByIdAsync(int id)
    {
        Lecture? lecture = await _lectureRepository.GetLectureById(id);
        if (lecture == null) { return null; }
        LectureDTO lecDTO = _lectureMapper.MapToDTO(lecture);
        //IEnumerable<User> teachers = await _lectureRepository.GetTeachersByCourseImplementationId(lecture.CourseImplementationId);
        //IEnumerable<string> teacherNames = from t in teachers select $"{t.FirstName} {t.LastName}";
        //lecDTO.TeacherNames = string.Join(", ", teacherNames);
        // DE 3 LINJENE OVER I EN EGEN FUNKSJON SOM TAR EN LECTURETDO SOM ARGUMENT
        await AddTeachers(lecDTO);
        return lecDTO; 
    }

    private async Task<LectureDTO> AddTeachers(LectureDTO lecDTO)
    {
        IEnumerable<User> teachers = await _lectureRepository.GetTeachersByCourseImplementationId(lecDTO.CourseImplementationId);
        IEnumerable<string> teacherNames = from t in teachers select $"{t.FirstName} {t.LastName}";
        lecDTO.TeacherNames = string.Join(", ", teacherNames);
        lecDTO.TeacherUserIds = (from t in teachers select t.Id).ToList();
        return lecDTO;
    }
}
