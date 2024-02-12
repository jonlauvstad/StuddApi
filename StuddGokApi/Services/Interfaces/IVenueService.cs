using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<VenueDTO>> GetAllVenuesAsync();
}
