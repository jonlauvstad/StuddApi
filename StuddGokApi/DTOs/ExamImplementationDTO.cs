using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class ExamImplementationDTO
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public int VenueId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    //public virtual Exam? Exam { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal DurationHours { get; set; }
    public int CourseImplementationId { get; set; }
    public string CourseImplementationName { get; set; } = string.Empty;
    public string CourseImplementationCode { get; set; } = string.Empty;
    //public virtual Venue? Venue { get; set; }
    public string VenueName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public virtual ICollection<UserExamImplementation> UserExamImplementation { get; set; } = new List<UserExamImplementation>();
    public string CourseImplementationLink { get => $"/CourseImplementation/{CourseImplementationId}"; }
    public string VenueLink { get => $"/Venue/{VenueId}"; }
    public string Link { get => $"/ExamImplementation/{Id}"; }

}
