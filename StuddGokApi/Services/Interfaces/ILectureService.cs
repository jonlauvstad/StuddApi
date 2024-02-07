using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface ILectureService
{
    Task<LectureDTO?> GetLectureByIdAsync(int id);
    Task<(LectureDTO? newLecture, EventDTO? venueEvent, LectureDTO? teacherLecture)> AddLectureAsync(LectureDTO lecture);
}
