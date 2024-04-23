using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;
    private readonly IMapper<Venue, VenueDTO> _venueMapper;
    private readonly IMapper<Location, LocationDTO> _locationMapper;

    public VenueService(IVenueRepository venueRepository, IMapper<Venue, VenueDTO> venueMapper, IMapper<Location, LocationDTO> locationMapper)
    {
        _venueRepository = venueRepository;
        _venueMapper = venueMapper;
        _locationMapper = locationMapper;
    }

    public async Task<VenueDTO?> AddVenueAsync(VenueDTO venueDTO)
    {
        Venue? venue = await _venueRepository.AddVenueAsync(_venueMapper.MapToModel(venueDTO));
        if (venue == null) return null;
        return _venueMapper.MapToDTO(venue);

    }

    public async Task<VenueDTO?> DeleteVenueAsync(int id)
    {
        Venue? venue = await _venueRepository.DeleteVenueAsync(id);
        if (venue == null) return null;
        return _venueMapper.MapToDTO(venue);
    }

    public async Task<IEnumerable<LocationDTO>> GetAllLocationsAsync()
    {
        return (await _venueRepository.GetAllLocationsAsync()).Select(x => _locationMapper.MapToDTO(x));

    }

    public async Task<IEnumerable<VenueDTO>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null)
    {
        IEnumerable<Venue> venues = await _venueRepository.GetAllVenuesAsync(availableFromTo);
        return from venue in venues select _venueMapper.MapToDTO(venue);
    }

    public async Task<VenueDTO?> GetVenueByIdAsync(int id)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(id);
        return venue is null ? null : _venueMapper.MapToDTO(venue);
    }

    public async Task<VenueDTO?> UpdateVenueAsync(int id, VenueDTO venueDTO)
    {
        Venue? venue = await _venueRepository.UpdateVenueAsync(id, _venueMapper.MapToModel(venueDTO));
        if (venue == null) return null;
        return _venueMapper.MapToDTO(venue);
    }
}
