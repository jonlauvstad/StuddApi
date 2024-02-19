using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class CourseMapper : IMapper<Course, CourseDTO>
{
    public CourseDTO MapToDTO(Course model)
    {
        return new CourseDTO
        {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            Points = model.Points,
            Category = model.Category,
            TeachCourse = model.TeachCours,
            DiplomaCourse = model.DiplomaCours,
            ExamCourse = model.ExamCours
        };
    }

    public Course MapToModel(CourseDTO dto)
    {
        return new Course
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            Points = dto.Points,
            Category = dto.Category,
            TeachCours = dto.TeachCourse,
            DiplomaCours = dto.DiplomaCourse,
            ExamCours = dto.ExamCourse
        };
    }
}
