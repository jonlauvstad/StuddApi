using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface ILectureRepository
{
    Task<Lecture?> GetLectureById(int id);
    Task<IEnumerable<User>> GetTeachersByCourseImplementationId(int courseImpId);
    Task<Lecture?> AddLectureAsync(Lecture lecture);
    Task<Lecture?> CheckTeacher(int courseImpId, DateTime from, DateTime to);
}
