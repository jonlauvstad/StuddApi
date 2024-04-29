using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, IMapper<User, UserDTO> userMapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _logger = logger;
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
            "UserService", "GetUserByIdAsync", "_userRepository.GetUserByIdAsync returns null", System.Diagnostics.Activity.Current?.Id);
        }
        return user != null ? _userMapper.MapToDTO(user) : null;
    }

    public async Task<IEnumerable<UserDTO>> GetUsersAsync(string? role)
    {
        IEnumerable<User> users = await _userRepository.GetUsersAsync(role);
        return from user in users select _userMapper.MapToDTO(user);     
    }
}

