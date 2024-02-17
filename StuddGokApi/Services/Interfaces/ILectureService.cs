using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface ILectureService
{
    Task<LectureDTO?> GetLectureByIdAsync(int id);
    Task<LectureBooking> AddLectureAsync(LectureDTO lecture);
    Task<LectureDTO?> DeleteLectureByIdAsync(int id);
    Task<LectureDTO?> UpdateLectureAsync(LectureDTO lecture);
}
