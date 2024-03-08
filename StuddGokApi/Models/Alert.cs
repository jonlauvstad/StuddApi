using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class Alert
{
    [Key] public int Id { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public string Message { get; set; } = string.Empty;
    [Required] public DateTime Time { get; set; }
    [Required] public bool Seen { get; set; }
    [Required] public string Links { get; set; } = string.Empty;

    public virtual User? User { get; set; }
}
