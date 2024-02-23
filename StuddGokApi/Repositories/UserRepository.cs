using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StuddGokDbContext _dbContext;

    public UserRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User?> GetUserByGokstadEmailAsync(string gokstadEmail)
    {
        IEnumerable<User> users = await _dbContext.Users.Where(x => x.GokstadEmail == gokstadEmail).ToListAsync();
        return users.FirstOrDefault();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(string? role)
    {
        IEnumerable<User> users = await _dbContext.Users.ToListAsync();
        if (role != null) users = users.Where(x => x.Role == role);
        return users;
    }
}
