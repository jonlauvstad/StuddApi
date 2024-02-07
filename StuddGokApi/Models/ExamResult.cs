using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class ExamResult
{
    [Key] public int Id { get; set; }
    [ForeignKey("ExamId")] public int ExamId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public string Grade { get; set; } = string.Empty;

    public virtual Exam? Exam { get; set; }
    public virtual User? User { get; set; }
}
