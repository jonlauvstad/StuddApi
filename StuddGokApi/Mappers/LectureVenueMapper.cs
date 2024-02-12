using StuddGokApi.DTOs;
using StuddGokApi.Models;
using System.Dynamic;

namespace StuddGokApi.Mappers;

public class LectureVenueMapper : IMapper<LectureVenue, LectureVenueDTO>
{
    public LectureVenueDTO MapToDTO(LectureVenue model)
    {
        LectureVenueDTO dto = new LectureVenueDTO()
        {
            LectureId = model.LectureId,
            VenueId = model.VenueId,
        };
        if (model.Venue != null)
        {
            dto.VenueName = model.Venue.Name;
            dto.VenueCapacity = model.Venue.Capacity;
        }
        return dto;
    }

    public LectureVenue MapToModel(LectureVenueDTO dto)
    {
        return new LectureVenue
        {
            LectureId = dto.LectureId,
            VenueId = dto.VenueId,
        };
    }
}
