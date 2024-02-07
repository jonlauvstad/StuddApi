using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class CourseImplementationMapper : IMapper<CourseImplementation, CourseImplementationDTO>
{
    public CourseImplementationDTO MapToDTO(CourseImplementation model)
    {
        return new CourseImplementationDTO
        {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            CourseId = model.CourseId,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };
    }

    public CourseImplementation MapToModel(CourseImplementationDTO dto)
    {
        return new CourseImplementation
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            CourseId = dto.CourseId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Semester = dto.Semester,
            EndSemester = dto.EndSemester,
            Year = dto.Year,
            EndYear = dto.EndYear
        };
    }

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Semester { get => StartDate.Month < 0 ? "V" : "H"; }
    public string EndSemester { get => EndDate.Month < 0 ? "V" : "H"; }
    public int Year { get => StartDate.Year; }
    public int EndYear { get => EndDate.Year; }

    // Added
    public string CourseLink { get => $"/Course/{CourseId}"; }
    public string Link { get => $"/CourseImplementation/{Id}"; }
}
