using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string GokstadEmail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email2 { get; set; } = string.Empty;
    public string Email3 { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Link { get => $"/User/{Id}"; }
}
