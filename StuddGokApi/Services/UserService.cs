using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class UserService : IUserService
{
    private readonly StuddGokDbContext _context;
    private readonly UserMapper _userMapper;

    public UserService(StuddGokDbContext context, UserMapper userMapper)
    {
        _context = context;
        _userMapper = userMapper;
    }
    public async Task<UserDTO> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
        return user != null ? _userMapper.MapToDTO(user) : null;
    }

}
