using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class LocationDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
