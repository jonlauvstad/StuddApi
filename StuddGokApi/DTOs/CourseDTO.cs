namespace StuddGokApi.DTOs;

public class CourseDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Points { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool TeachCourse { get; set; }
    public bool DiplomaCourse { get; set; }
    public bool ExamCourse { get; set; }
    public string Link { get => $"/Course/{Id}"; }
}
