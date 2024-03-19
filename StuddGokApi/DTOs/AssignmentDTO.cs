using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class AssignmentDTO
{
    public int Id { get; set; }
    public int CourseImplementationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public bool Mandatory { get; set; }

    public ICollection<AssignmentResult> AssignmentResults { get; set; } = new List<AssignmentResult>();
    //public CourseImplementation? CourseImplementation { get; set; }   Removed pga cycle in serialization
    public string CourseImplementationLink { get => $"/CourseImplementation/{CourseImplementationId}"; } 
    public string Link { get => $"/Assignment/{Id}"; }

    // ADDED
    public string CourseImplementationName { get; set; } = string.Empty;
    public string CourseImplementationCode { get; set; } = string.Empty;
}
