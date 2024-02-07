using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class StudyProgram
{
    [Key] public int Id { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public decimal Points { get; set; }
    [Required] public string Level {  get; set; } = string.Empty;
    [Required] public int NUS_code { get; set; }

    public virtual ICollection<ProgramImplementation> ProgramImplementations { get; set; } = new HashSet<ProgramImplementation>();

}
