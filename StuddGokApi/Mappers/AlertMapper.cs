using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StuddGokApi.Mappers;

public class AlertMapper : IMapper<Alert, AlertDTO>
{
    public AlertDTO MapToDTO(Alert model)
    {
        return new AlertDTO
        {
            Id = model.Id,
            UserId = model.UserId,
            Message = model.Message,
            Time = model.Time,
            Seen = model.Seen
        };
    }

    public Alert MapToModel(AlertDTO dto)
    {
        return new Alert
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Message = dto.Message,
            Time = dto.Time,
            Seen = dto.Seen
        };
    }
}
