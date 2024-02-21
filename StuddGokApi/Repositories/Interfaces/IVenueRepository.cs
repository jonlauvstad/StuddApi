using StuddGokApi.DTMs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<Event?> CheckVenueAsync(int venueId, DateTime from, DateTime to);
    Task<Venue?> GetVenueByIdAsync(int id);
    Task<LectureVenue?> AddLectureVenueAsync(LectureVenue lectureVenue);
    Task<IEnumerable<Venue>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null);
}
