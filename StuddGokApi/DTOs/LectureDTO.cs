using StuddGokApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.DTOs;

public class LectureDTO
{
    public int Id { get; set; }
    public int CourseImplementationId { get; set; }
    public string Theme { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    //public ICollection<LectureVenue> LectureVenues { get; set; } = new List<LectureVenue>();
    public IEnumerable<int> VenueIds { get; set; } = new List<int>();   
    public IEnumerable<string> VenueNamesList { get; set; } = new List<string>();
    public string VenueNames { get => string.Join(", ", VenueNamesList); }
    //public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public IEnumerable<int> AttendanceIds { get; set; } = new List<int>();
    //public CourseImplementation? CourseImplementation { get; set; }
    public string CourseImplementationLink { get => $"/CourseImplementation/{CourseImplementationId}"; }
    public string Link { get => $"/Lecture/{Id}"; }
    public string Duration { get {
            TimeSpan timeDifference = EndTime - StartTime;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes;
            string h = "";
            if (hours != 0) { h = $"{hours} time(r)"; }
            string m = "";
            if (minutes != 0) { m = $"{minutes} minutter"; }
            return $"{h} {m}";
        } 
    }

    // ADDED
    public string CourseImplementationName { get; set; } = string.Empty;
    public string CourseImplementationCode { get; set; } = string.Empty;
    public string TeacherNames { get; set; } = string.Empty;
    public List<int> TeacherUserIds { get; set; } = new List<int>();
    public List<int> ProgramTeacherUserIds { get; set; } = new List<int>();
}
