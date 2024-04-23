using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using StuddGokApi.Configuration;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly StuddGokDbContext _dbContext;
    private readonly IOptions<HomeVenue> _homeVenueConfig;

    public VenueRepository(StuddGokDbContext dbContext, IOptions<HomeVenue> homeVenueConfig)
    {
        _dbContext = dbContext;
        _homeVenueConfig = homeVenueConfig;
    }

    public async Task<LectureVenue?> AddLectureVenueAsync(LectureVenue lectureVenue)
    {
        EntityEntry e = await _dbContext.LectureVenues.AddAsync(lectureVenue);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is LectureVenue) { return (LectureVenue)o; }
        return null;
    }

    public async Task<Venue?> DeleteVenueAsync(int id)
    {
        Venue? venue = await GetVenueByIdAsync(id);
        if (venue == null) return null;
        int numDeleted = await _dbContext.Venues.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (numDeleted == 0)
        {
            // Logging
            return null;
        }
        return venue;
    }

    public async Task<Venue?> UpdateVenueAsync(int id, Venue venue)
    {
        int i = await _dbContext.Venues.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(v => v.Name, venue.Name)
            .SetProperty(v => v.Description, venue.Description)
            .SetProperty(v => v.LocationId, venue.LocationId)
            .SetProperty(v => v.StreetAddress, venue.StreetAddress)
            .SetProperty(v => v.PostCode, venue.PostCode)
            .SetProperty(v => v.City, venue.City)
            .SetProperty(v => v.Capacity, venue.Capacity)
        );
        if (i == 0)
        {
            // Logging
            return null;
        }
        return await _dbContext.Venues.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Venue?> AddVenueAsync(Venue venue)
    {
        EntityEntry e = await _dbContext.Venues.AddAsync(venue);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (!(o is Venue)) return null;
        return await _dbContext.Venues
            .Include(v => v.Location)
            .FirstOrDefaultAsync(x =>  x.Id == ((Venue)o).Id);
    }

    public async Task<Event?> CheckVenueAsync(int venueId, DateTime from, DateTime to)
    {
        if (venueId == _homeVenueConfig.Value.Id) return null; 

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

    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
    {
        return await _dbContext.Locations.ToListAsync();
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

    public async Task TestAddMultipleAsync()
    {
        IEnumerable<LectureVenue> lecVens = new List<LectureVenue>();
        await _dbContext.LectureVenues.AddRangeAsync(lecVens);
        await _dbContext.SaveChangesAsync();
       
    }
}
