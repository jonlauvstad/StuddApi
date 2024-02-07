using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class TeacherProgram
{
    [Key] public int Id { get; set; }
    [ForeignKey("ProgramImplementationId")] public int ProgramImplementationId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public virtual ProgramImplementation? ProgramImplementation { get; set; }
    public virtual User? User { get; set; }
}
