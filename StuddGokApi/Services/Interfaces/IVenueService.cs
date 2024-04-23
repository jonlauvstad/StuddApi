using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<VenueDTO>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null);

    Task<VenueDTO?> GetVenueByIdAsync(int id);
    Task<VenueDTO?> AddVenueAsync(VenueDTO venueDTO);
    Task<VenueDTO?> DeleteVenueAsync(int id);
    Task<VenueDTO?> UpdateVenueAsync(int id, VenueDTO venueDTO);

    Task<IEnumerable<LocationDTO>> GetAllLocationsAsync();
}
