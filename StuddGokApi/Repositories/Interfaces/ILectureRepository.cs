using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ILectureRepository
{
    Task<Lecture?> GetLectureById(int id);
    Task<IEnumerable<User>> GetTeachersByCourseImplementationId(int courseImpId);
    Task<IEnumerable<User>> GetProgramTeachersByCourseImplementationId(int courseImpId);
    Task<Lecture?> AddLectureAsync(Lecture lecture);
    Task<(Lecture?, LectureVenue?)> AddLectureAndVenueAsync(Lecture lecture, int venueId);
    Task<Lecture?> CheckTeacher(int courseImpId, DateTime from, DateTime to);
    Task<Lecture?> DeleteLectureByIdAsync(int id);
    Task<Lecture?> UpdateLectureAsync(Lecture lecture);
    Task<Lecture?> UpdateLectureAndVenueAsync(Lecture lecture, int venueId);
    Task<bool> IsOwner(int userId, string role, int lectureId, int? courseImplementationId = null);
}
