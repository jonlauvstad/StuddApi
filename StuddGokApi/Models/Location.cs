using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class Location
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public virtual ICollection<Venue> Venues { get; set; } = new HashSet<Venue>();
}
