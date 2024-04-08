using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class ExamGroupDTO
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string ExamLink { get => $"/Exam/{ExamId}"; }
    public string UserLink  { get => $"/User/{UserId}"; }

    public int CourseImplementationId { get; set; }
    public string CourseImplementationCode { get; set; } = string.Empty;
    public string CourseImplementationName { get; set; } = string.Empty;
    public string CourseImplementationLink { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string GokstadEmail { get; set; } = string.Empty;
}
