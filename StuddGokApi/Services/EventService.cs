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
    private readonly ILogger<EventService> _logger;

    public EventService(IEventRepository eventRepository, IMapper<Event, EventDTO> eventMapper, ILogger<EventService> logger)
    {
        _eventRepository = eventRepository;
        _eventMapper = eventMapper;
        _logger = logger;
    }

    public async Task<ICollection<EventDTO>> GetAllEventsAsync(string? type, DateTime? from_, DateTime? to)
    {


        return (from ev in await _eventRepository.GetAllEventsAsync(type, from_, to) select _eventMapper.MapToDTO(ev)).ToList();
    }

    public async Task<ICollection<EventDTO>?> GetEventsAsync(int userId, string type, DateTime? from, DateTime? to, int user_id, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;


        if (userId != user_id && role != "admin")
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "EventService", "GetEventsAsync", "_eventRepository.IsOwner returns false", traceId);
            
            return null;
        }
        ICollection<Event> events = await _eventRepository.GetEventsAsync(userId, type, from, to);
        return (from e in events select _eventMapper.MapToDTO(e)).ToList(); 
    }
}
