using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByGokstadEmailAsync(string gokstadEmail);

    Task<User?> GetUserByIdAsync(int userId);
    Task<IEnumerable<User>> GetUsersAsync(string? role);
}
