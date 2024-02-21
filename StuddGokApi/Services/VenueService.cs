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

    public VenueService(IVenueRepository venueRepository, IMapper<Venue, VenueDTO> venueMapper)
    {
        _venueRepository = venueRepository;
        _venueMapper = venueMapper;
    }


    public async Task<IEnumerable<VenueDTO>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null)
    {
        IEnumerable<Venue> venues = await _venueRepository.GetAllVenuesAsync(availableFromTo);
        return from venue in venues select _venueMapper.MapToDTO(venue);
    }
}
