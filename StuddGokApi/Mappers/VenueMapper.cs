using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class VenueMapper : IMapper<Venue, VenueDTO>
{
    public VenueDTO MapToDTO(Venue model)
    {
        return new VenueDTO
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            LocationId = model.LocationId,
            StreetAddress = model.StreetAddress,
            PostCode = model.PostCode,
            City = model.City,
            Capacity = model.Capacity,

            // From virtual
            LocationName = model.Location!.Name,
        };
    }

    public Venue MapToModel(VenueDTO dto)
    {
        return new Venue
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            LocationId = dto.LocationId,
            StreetAddress = dto.StreetAddress,
            PostCode = dto.PostCode,
            City = dto.City,
            Capacity = dto.Capacity,
        };
    }
}
