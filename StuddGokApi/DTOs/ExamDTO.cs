using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class ExamDTO
{
    public int Id { get; set; }
    public int CourseImplementationId { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal DurationHours { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    public string CourseImplementationCode { get; set; } = string.Empty;
    public string CourseImplementationName { get; set; } = string.Empty;

    public List<int> ExamImplementationIds { get; set; } = new List<int>();
    public List<int> ExamResultIds { get; set; } = new List<int>();
    
    public string Link { get => $"/Exam/{Id}"; }
    public  string CourseImplementationLink { get => $"/CourseImplementation/{CourseImplementationId}"; }
}
