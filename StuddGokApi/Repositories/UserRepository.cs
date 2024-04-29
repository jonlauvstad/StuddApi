using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories;

public class UserRepository : IUserRepository
{
    // ABOUT LOGGING: No logger in this class, since it would not add to the information captured in the service layer
    // - funcs here returning null. The logging in the service layer has references to the functions here.
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
