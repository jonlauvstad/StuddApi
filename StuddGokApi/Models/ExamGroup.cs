using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuddGokApi.Models;

public class ExamGroup
{
    [Key] public int Id { get; set; }
    [ForeignKey("ExamId")] public int ExamId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public virtual Exam? Exam { get; set; }
    public virtual User? User { get; set; }
}
