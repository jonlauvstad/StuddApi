using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly StuddGokDbContext _dbContext;

    public VenueRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LectureVenue?> AddLectureVenueAsync(LectureVenue lectureVenue)
    {
        EntityEntry e = await _dbContext.LectureVenues.AddAsync(lectureVenue);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is LectureVenue) { return (LectureVenue)o; }
        return null;
    }

    public async Task<Event?> CheckVenueAsync(int venueId, DateTime from, DateTime to)
    {
        List<Event> events = new List<Event>();

        IEnumerable<LectureVenue> lvs = await _dbContext.LectureVenues.Where(x => x.VenueId == venueId).ToListAsync();
        IEnumerable<Lecture> lectures = from lv in lvs select lv.Lecture;
        events.AddRange(from l in lectures 
                        select new Event
                        {
                            UnderlyingId = l.Id,
                            Time = l.StartTime,
                            Type = "Forelesning",
                            TypeEng = "Lecture",
                            CourseImplementationId = l.CourseImplementationId,
                            CourseImpCode = l.CourseImplementation!.Code,
                            CourseImpName = l.CourseImplementation!.Name,
                            TimeEnd = l.EndTime,
                        });

        IEnumerable<ExamImplementation> eims = await _dbContext.ExamImplementations.Where(x => x.VenueId==venueId).ToListAsync();
        events.AddRange(from e in eims
                        select new Event
                        {
                            UnderlyingId = e.Id,
                            Time = e.StartTime,
                            Type = "Eksamen",
                            TypeEng = "ExamImplementation",
                            CourseImplementationId = e.Exam!.CourseImplementationId,
                            CourseImpCode = e.Exam!.CourseImplementation!.Code,
                            CourseImpName = e.Exam!.CourseImplementation!.Name,
                            TimeEnd = e.EndTime,
                        });
        IEnumerable<Event> evs = events.Where(x => x.Time < to && x.TimeEnd > from);
        if (evs.Any()) { return evs.First(); }
        return null;
    }

    public async Task<IEnumerable<Venue>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null)
    {
        IEnumerable<Venue> venues = await _dbContext.Venues.ToListAsync();
        if (availableFromTo == null)
        {
            return venues.OrderBy(x => x.Name);
        }

        // ExamImplementations
        IEnumerable<ExamImplementation> eis =
            await _dbContext.ExamImplementations.Where(x => x.StartTime < availableFromTo.Value.to && x.EndTime > availableFromTo.Value.from)
                .ToListAsync();
        List<Venue> occupiedVenues = (from ei in eis select ei.Venue).ToList();

        // Lectures
        IEnumerable<LectureVenue> lvs =
            await _dbContext.LectureVenues.Where(x => x.Lecture!.StartTime < availableFromTo.Value.to &&
            x.Lecture.EndTime > availableFromTo.Value.from).ToListAsync();
        occupiedVenues.AddRange(from lv in lvs  select lv.Venue);

        return venues.Where(x => !occupiedVenues.Contains(x));
        
    }

    public async Task<Venue?> GetVenueByIdAsync(int id)
    {
        IEnumerable<Venue> vs = await _dbContext.Venues.Where(x => x.Id == id).ToListAsync();

        Venue? v = vs.FirstOrDefault();
        if (v == null) { return null; }
        return v;
    }
}
