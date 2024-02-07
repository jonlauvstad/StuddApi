using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class UserExamImplementation
{
    [Key] public int Id { get; set; }
    [ForeignKey("ExamImplementationId")] public int ExamImplementationId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public virtual ExamImplementation? ExamImplementation { get; set; }
    public virtual User? User { get; set; }
}
