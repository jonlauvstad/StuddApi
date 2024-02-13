using StuddGokApi.Models;
using System.Text;

namespace StuddGokApi.DTOs;

public class LectureBooking
{
    public int LectureId { get; set; }  
    public int NumStudents { get; set; }
    public int VenueCapacity { get; set; }
    public string VenueName { get; set; }
    public Dictionary<string, string> Links { get; }
    public bool Success { get; }
    public string Message { get; }

    public LectureBooking(LectureDTO? newLecture, EventDTO? venueEvent, LectureDTO? teacherLecture, 
        Venue? venue, CourseImplementationDTO? courseImpDTO, string? failMsg=null) 
    {
        LectureId = newLecture == null ? 0 : newLecture.Id;
        Success = newLecture != null;
        NumStudents = courseImpDTO == null ? 0 : courseImpDTO.NumStudents;
        VenueCapacity = venue == null ? 0 : venue.Capacity;
        VenueName = venue == null ? string.Empty : venue.Name;

        Links = new Dictionary<string, string>()
        {
            {"NewLecture", newLecture == null ? "" : newLecture.Link},
            {"VenueEvent", venueEvent == null ? "" : venueEvent.Link },
            {"TeacherLecture", teacherLecture == null ? "" : teacherLecture.Link },
        };
        if (failMsg != null)
        {
            Message = failMsg;
            //return;
        }
        else
        {
            // Creating message
            StringBuilder sb = new StringBuilder();
            string room = venue == null ? "Ikke booket" : venue.Name;
            
            if (newLecture != null)
            {
                sb.Append($"Vellykket registrering av forelesning.<br>" +
                    $"Id: {newLecture.Id}<br>" +
                    $"Kursgjennomføring: {newLecture.CourseImplementationCode}<br>" +
                    $"Rom: {room}<br>" +
                    $"Start: {newLecture.StartTime}<br>" +
                    $"Slutt: {newLecture.EndTime}<br>" +
                    $"Studenter: {NumStudents}<br>" +
                    $"Kapasitet:{VenueCapacity}<br>");

                if (NumStudents > VenueCapacity)
                {
                    sb.Append("Antallet studenter overstiger rommets kapasitet!");
                }
            }
            else
            {
                sb.Append($"Kunne ikke registrere forelesningen.<br>");
                if (teacherLecture != null)
                {
                    sb.Append($"Læreren er opptatt: {teacherLecture.Link}<br>");
                }
                if (venueEvent != null)
                {
                    sb.Append($"Rommet er opptatt: {venueEvent.Link}<br>");
                }
            }
            Message = sb.ToString();
        }
        
    }

    
}
