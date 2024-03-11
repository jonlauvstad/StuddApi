using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class EventRepository : IEventRepository
{
    private readonly StuddGokDbContext _dbContext;

    public EventRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Event>> GetAllEventsAsync(string? type, DateTime? from, DateTime? to)
    {
        List<Assignment> assignments = await _dbContext.Assignments.ToListAsync();
        List<Lecture> lectures = await _dbContext.Lectures.ToListAsync();
        List<ExamImplementation> examImps = await _dbContext.ExamImplementations.ToListAsync();

        List<Event> events = new List<Event>();

        foreach (Assignment a in assignments)
        {
            events.Add(
                new Event
                {
                    UnderlyingId = a.Id,
                    Time = a.Deadline,
                    Type = "Arbeidskrav",
                    TypeEng = "Assignment",
                    CourseImplementationId = a.CourseImplementationId,
                    CourseImpCode = a.CourseImplementation!.Code,
                    CourseImpName = a.CourseImplementation!.Name,
                }
            );
        }

        foreach (Lecture l in lectures)
        {
            LectureVenue? lecVen = await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.LectureId == l.Id);
            Venue? v = lecVen == null ? null : lecVen.Venue;
            events.Add(
                new Event
                {
                    UnderlyingId = l.Id,
                    Time = l.StartTime,
                    Type = "Forelesning",
                    TypeEng = "Lecture",
                    CourseImplementationId = l.CourseImplementationId,
                    CourseImpCode = l.CourseImplementation!.Code,
                    CourseImpName = l.CourseImplementation!.Name,
                    TimeEnd = l.EndTime,
                    VenueId = v == null ? 0 : v.Id,
                    VenueName = v == null ? string.Empty : v.Name,
                    VenueCapacity = v == null ? 0 : v.Capacity,
                }
            );
        }

        foreach (ExamImplementation e in examImps)
        {
            Venue? v = e.Venue;
            events.Add(
                new Event
                {
                    UnderlyingId = e.Id,
                    Time = e.StartTime,
                    Type = "Eksamen",
                    TypeEng = "ExamImplementation",
                    CourseImplementationId = e.Exam!.CourseImplementationId,
                    CourseImpCode = e.Exam!.CourseImplementation!.Code,
                    CourseImpName = e.Exam!.CourseImplementation!.Name,
                    TimeEnd = e.EndTime,
                    VenueId = v == null ? 0 : v.Id,
                    VenueName = v == null ? string.Empty : v.Name,
                    VenueCapacity = v == null ? 0 : v.Capacity,
                }
            );
        }
        if (from != null) events = events.Where(x => x.Time >= from).ToList();
        if (to != null) events = events.Where(x => x.Time <= to).ToList();
        if (type != null) events = events.Where(x => x.Type == type).ToList();

        return events.OrderBy(x => x.Time).ToList();
    }

    public async Task<ICollection<Event>> GetEventsAsync(int userId, string? type, DateTime? from, DateTime? to)
    {
        IEnumerable<StudentProgram> studProgs = await _dbContext.StudentPrograms.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> progImpIds = from sp in studProgs select sp.ProgramImplementationId;
        IEnumerable<ProgramCourse> courseIds = await _dbContext.ProgramCourses.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        IEnumerable<CourseImplementation> courseImps = from cp in courseIds select cp.CourseImplementation;

        List<Assignment> assignments = new List<Assignment>();
        foreach (CourseImplementation cs in courseImps)
        {
            assignments.AddRange(cs.Assignments);
        }

        List<Lecture> lectures = new List<Lecture>();
        foreach (CourseImplementation cs in courseImps)
        {
            lectures.AddRange(cs.Lectures);
        }

        //List<ExamImplementation> examImps = new List<ExamImplementation>();
        //foreach (CourseImplementation cs in courseImps)
        //{
        //    foreach(Exam e in cs.Exams)
        //    {
        //        examImps.AddRange(e.ExamImplementation);
        //    }
        //}

        IEnumerable<UserExamImplementation> userExamImps = await _dbContext.UserExamImplementations.Where(x => x.UserId == userId).ToListAsync();
        List<ExamImplementation> examImps = new List<ExamImplementation>();
        foreach (UserExamImplementation uei in userExamImps)
        {
            if (uei.UserId == userId) { examImps.Add(uei.ExamImplementation!); }
        }

        IEnumerable<TeacherCourse> teacherCourses = await _dbContext.TeacherCourses.Where(x => x.UserId == userId).ToListAsync();   // !=
        IEnumerable<CourseImplementation> tCourseImps = from item in teacherCourses select item.CourseImplementation;
        List<Lecture> tLectures = new List<Lecture>();
        foreach (CourseImplementation cs in tCourseImps) { tLectures.AddRange(cs.Lectures); }
        tLectures = (from lec in tLectures where !lectures.Contains(lec) select lec).ToList();

        List < Event > events = new List<Event>();
        
        foreach (Assignment a in assignments)
        {
            events.Add(
                new Event
                {
                    UnderlyingId = a.Id,
                    Time = a.Deadline,
                    Type = "Arbeidskrav",
                    TypeEng = "Assignment",
                    CourseImplementationId = a.CourseImplementationId,
                    CourseImpCode = a.CourseImplementation!.Code,
                    CourseImpName = a.CourseImplementation!.Name,
                }
            );
        }

        foreach (Lecture l in lectures)
        {
            LectureVenue? lecVen = await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.LectureId == l.Id);
            Venue? v = lecVen == null ? null : lecVen.Venue;
            events.Add(
                new Event
                {
                    UnderlyingId = l.Id,
                    Time = l.StartTime,
                    Type = "Forelesning",
                    TypeEng = "Lecture",
                    CourseImplementationId = l.CourseImplementationId,
                    CourseImpCode = l.CourseImplementation!.Code,
                    CourseImpName = l.CourseImplementation!.Name,
                    TimeEnd = l.EndTime,
                    VenueId = v == null ? 0 : v.Id,
                    VenueName = v == null ? string.Empty : v.Name,
                    VenueCapacity = v == null ? 0 : v.Capacity,
                }
            );
        }

        foreach (ExamImplementation e in examImps)
        {
            Venue? v = e.Venue;
            events.Add(
                new Event
                {
                    UnderlyingId = e.Id,
                    Time = e.StartTime,
                    Type = "Eksamen",
                    TypeEng = "ExamImplementation",
                    CourseImplementationId = e.Exam!.CourseImplementationId,
                    CourseImpCode = e.Exam!.CourseImplementation!.Code,
                    CourseImpName = e.Exam!.CourseImplementation!.Name,
                    TimeEnd = e.EndTime,
                    VenueId = v==null ? 0 : v.Id,
                    VenueName = v==null ? string.Empty : v.Name,
                    VenueCapacity = v==null ? 0 : v.Capacity,
                }
            );
        }

        foreach (Lecture l in tLectures)
        {
            LectureVenue? lecVen = await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.LectureId == l.Id);
            Venue? v = lecVen==null ? null : lecVen.Venue;
            events.Add(
                new Event
                {
                    UnderlyingId = l.Id,
                    Time = l.StartTime,
                    Type = "Undervisning",
                    TypeEng = "Lecture",
                    CourseImplementationId = l.CourseImplementationId,
                    CourseImpCode = l.CourseImplementation!.Code,
                    CourseImpName = l.CourseImplementation!.Name,
                    VenueId = v == null ? 0 : v.Id,
                    VenueName = v == null ? string.Empty : v.Name,
                    VenueCapacity = v == null ? 0 : v.Capacity,
                }
            );
        }

        return events.OrderBy(x => x.Time).ToList();
    }
}
