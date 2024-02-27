using Microsoft.AspNetCore.Mvc;
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
    Task<IEnumerable<LectureDTO>> GetLecturesAsync(DateTime? startAfter, DateTime? endBy, int? courseImpId,
        int? venueId, int? teacherId);
    Task<IEnumerable<LectureDTO>?> DeleteMultipleAsync(string id_string);
}
