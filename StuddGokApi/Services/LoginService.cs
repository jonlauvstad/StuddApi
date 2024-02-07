using Microsoft.IdentityModel.Tokens;
using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StuddGokApi.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;
    private readonly IMapper<User, UserDTO> _userMapper;

    public LoginService(IUserRepository userRepo, IConfiguration configuration, IMapper<User, UserDTO> userMApper)
    {
        _userRepo = userRepo;
        _configuration = configuration;
        _userMapper = userMApper;
    }

    public async Task<string?> LoginAsync(LoginDTO loginDTO)
    {
        User? user = await _userRepo.GetUserByGokstadEmailAsync(loginDTO.GokstadEmail);
        if (user == null) { return null; }
        if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.HashedPassword)) { return null; }
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        UserDTO userDTO = _userMapper.MapToDTO(user);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: new[]
            {
                new Claim("gokstademail", userDTO.GokstadEmail),
                new Claim("firstname", userDTO.FirstName),
                new Claim("lastname", userDTO.LastName),
                new Claim("id", userDTO.Id.ToString()),
                new Claim("role", userDTO.Role),
                new Claim("email2", userDTO.Email2),
                new Claim("email3", userDTO.Email3),
                new Claim("link", userDTO.Link),
                new Claim(ClaimTypes.Role, userDTO.Role),
                new Claim(ClaimTypes.NameIdentifier, userDTO.GokstadEmail)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
