using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class ProgramLocation
{
    [Key] public int Id { get; set; }
    [ForeignKey("ProgramImplementationId")] public int ProgramImplementationId { get; set; }
    [ForeignKey("LocationId")] public int LocationId { get; set; }

    public virtual ProgramImplementation? ProgramImplementation { get; set; }
    public virtual Location? Location { get; set; }
}
