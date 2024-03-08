using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class AlertDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public bool Seen { get; set; }
    public string Links { get; set; } = string.Empty;

    //public virtual User? User { get; set; }
}
