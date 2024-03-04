using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class EventMapper : IMapper<Event, EventDTO>
{
    public EventDTO MapToDTO(Event model)
    {
        return new EventDTO
        {
            Time = model.Time,
            UnderlyingId = model.UnderlyingId,
            Type = model.Type,
            TypeEng = model.TypeEng,
            CourseImplementationId = model.CourseImplementationId,
            CourseImpCode = model.CourseImpCode,
            CourseImpName = model.CourseImpName,
            CourseImplementation = model.CourseImplementation,
            TimeEnd = model.TimeEnd,
            VenueId = model.VenueId,
            VenueName = model.VenueName,
            VenueCapacity = model.VenueCapacity,
        };
    }


    public Event MapToModel(EventDTO dto)
    {
        return new Event
        {
            Time = dto.Time,
            UnderlyingId = dto.UnderlyingId,
            Type = dto.Type,
            TypeEng = dto.TypeEng,
            CourseImplementationId = dto.CourseImplementationId,
            CourseImpCode = dto.CourseImpCode,
            CourseImpName = dto.CourseImpName,
            CourseImplementation = dto.CourseImplementation,
            TimeEnd = dto.TimeEnd,
            VenueId = dto.VenueId,
            VenueName = dto.VenueName,
            VenueCapacity = dto.VenueCapacity,
        };
    }
}
