using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Services.Interfaces;

public interface ILectureService
{
    Task<LectureDTO?> GetLectureByIdAsync(int id);
    Task<LectureBooking> AddLectureAsync(LectureDTO lecture, int userId, string role);
    Task<LectureDTO?> DeleteLectureByIdAsync(int id, int userId, string role);
    Task<LectureDTO?> UpdateLectureAsync(LectureDTO lecture, int userId, string role);
}
