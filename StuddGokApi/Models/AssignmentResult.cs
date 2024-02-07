using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class AssignmentResult
{
    [Key] public int Id { get; set; }
    [ForeignKey("AssignmentId")] public int AssignmentId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public string Grade { get; set; } = string.Empty;

    public virtual Assignment? Assignment { get; set; }
    public virtual User? User { get; set; }
}
