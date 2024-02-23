using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO> GetUserByIdAsync(int id);
    public Task<IEnumerable<UserDTO>> GetUsersAsync(string? role);
}
