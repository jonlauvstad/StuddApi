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


    public async Task<IEnumerable<VenueDTO>> GetAllVenuesAsync()
    {
        IEnumerable<Venue> venues = await _venueRepository.GetAllVenuesAsync();
        return from venue in venues select _venueMapper.MapToDTO(venue);
    }
}
