using StuddGokApi.DTMs;

namespace StuddGokApi.Repositories.Interfaces;

public interface IVenueRepository
{
    Task<Event?> CheckVenue(int venueId, DateTime from, DateTime to); 
}
