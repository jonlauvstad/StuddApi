using StuddGokApi.Models;
using System;
using System.Text;

namespace StuddGokApi.DTOs;

public class LectureBooking
{
    public int LectureId { get; set; }  
    public string CourseImplementationCode { get; }
    public int NumStudents { get; set; }
    public int VenueCapacity { get; set; }
    public string VenueName { get; set; }
    public Dictionary<string, string> Links { get; } = new Dictionary<string, string>();
    public bool Success { get; }
    public string Message { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public bool Room {  get; }
    public string RoomString { get; } 
    public string LectureLink { get; }

    public LectureBooking(LectureDTO? newLecture, EventDTO? venueEvent, LectureDTO? teacherLecture, 
        Venue? venue, CourseImplementationDTO? courseImpDTO, string? failMsg=null) 
    {
        LectureId = newLecture == null ? 0 : newLecture.Id;
        CourseImplementationCode = newLecture == null ? "" : newLecture.CourseImplementationCode;
        Success = newLecture != null;
        NumStudents = courseImpDTO == null ? 0 : courseImpDTO.NumStudents;
        VenueCapacity = venue == null ? 0 : venue.Capacity;
        VenueName = venue == null ? string.Empty : venue.Name;
        Room = venue == null ? false : true;
        RoomString = venue == null ? "Ikke booket" : venue.Name;
        LectureLink = newLecture == null ? "" : newLecture.Link;

        if (newLecture != null)
        {
            StartTime = newLecture.StartTime;
            EndTime = newLecture.EndTime;
            Links["NewLecture"] =  newLecture.Link;
        }
        else
        {
            if(venueEvent != null) { Links["VenueEvent"] = venueEvent.Link; }
            if(teacherLecture != null) { Links["TeacherLecture"] = teacherLecture.Link; }
        }

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
                sb.Append($"Vellykket registrering av forelesning.<br>");

                if (Room && NumStudents > VenueCapacity)
                {
                    sb.Append($"<label style='color: orangered;'>Antallet studenter ({NumStudents}) overstiger rommets kapasitet ({VenueCapacity})!</label>");
                }
            }
            else
            {
                sb.Append($"Kunne ikke registrere forelesningen.<br>");
                if (teacherLecture != null)
                {
                    sb.Append($"Læreren er opptatt<br>");       // : {teacherLecture.Link}
                }
                if (venueEvent != null)
                {
                    sb.Append($"Rommet er opptatt<br>");        // : {venueEvent.Link}
                }
            }
            Message = sb.ToString();
        }
        
    }

    
}
