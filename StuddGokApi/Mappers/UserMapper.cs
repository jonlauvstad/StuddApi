using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User model)
    {
        return new UserDTO
        {
            Id = model.Id,
            GokstadEmail = model.GokstadEmail,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email2 = model.Email2,
            Email3 = model.Email3,
            Role = model.Role,
        };
    }

    public User MapToModel(UserDTO dto)
    {
        return new User
        {
            Id = dto.Id,
            GokstadEmail = dto.GokstadEmail,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email2 = dto.Email2,
            Email3 = dto.Email3,
            Role = dto.Role,
        };
    }
}
