using StuddGokApi.Models;

namespace StuddGokApi.DTMs;

public class Event
{
    public DateTime Time { get; set; }
    public int UnderlyingId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string TypeEng {  get; set; } = string.Empty;
    public int CourseImplementationId { get; set; }
    public string CourseImpCode { get; set; } = string.Empty;
    public string CourseImpName { get; set; } = string.Empty;
    public CourseImplementation? CourseImplementation { get; set; }
    public DateTime TimeEnd { get; set; }

}
