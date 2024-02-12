using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ILectureRepository
{
    Task<Lecture?> GetLectureById(int id);
    Task<IEnumerable<User>> GetTeachersByCourseImplementationId(int courseImpId);
    Task<Lecture?> AddLectureAsync(Lecture lecture);
    Task<(Lecture?, LectureVenue?)> AddLectureAndVenueAsync(Lecture lecture, int venueId);
    Task<Lecture?> CheckTeacher(int courseImpId, DateTime from, DateTime to);
    Task<Lecture?> DeleteLectureByIdAsync(int id);
}
