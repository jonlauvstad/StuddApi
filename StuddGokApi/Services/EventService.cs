using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper<Event, EventDTO> _eventMapper;

    public EventService(IEventRepository eventRepository, IMapper<Event, EventDTO> eventMapper)
    {
        _eventRepository = eventRepository;
        _eventMapper = eventMapper;
    }

    public async Task<ICollection<EventDTO>> GetAllEventsAsync(string? type, DateTime? from_, DateTime? to)
    {
        return (from ev in await _eventRepository.GetAllEventsAsync(type, from_, to) select _eventMapper.MapToDTO(ev)).ToList();
    }

    public async Task<ICollection<EventDTO>?> GetEventsAsync(int userId, string type, DateTime? from, DateTime? to, int user_id, string role)
    {
        if(userId != user_id && role != "admin")
        {
            return null;
        }
        ICollection<Event> events = await _eventRepository.GetEventsAsync(userId, type, from, to);
        return (from e in events select _eventMapper.MapToDTO(e)).ToList(); 
    }
}
