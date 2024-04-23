using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers
{
    public class LocationMapper : IMapper<Location, LocationDTO>
    {
        public LocationDTO MapToDTO(Location model)
        {
            return new LocationDTO
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public Location MapToModel(LocationDTO dto)
        {
            return new Location
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
