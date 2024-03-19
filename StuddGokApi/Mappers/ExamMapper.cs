using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class ExamMapper : IMapper<Exam, ExamDTO>
{
    public ExamDTO MapToDTO(Exam model)
    {
        return new ExamDTO
        {
            Id = model.Id,
            CourseImplementationId = model.CourseImplementationId,
            Category = model.Category,
            DurationHours = model.DurationHours,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,

            CourseImplementationCode = model.CourseImplementation!.Code,
            CourseImplementationName = model.CourseImplementation!.Name,

            ExamImplementationIds = (from exImp in model.ExamImplementation select exImp.Id).ToList(),
            ExamResultIds = model.ExamResults.Select(x => x.Id).ToList(),
        };
    }

    public Exam MapToModel(ExamDTO dto)
    {
        return new Exam
        {
            Id = dto.Id,
            CourseImplementationId = dto.CourseImplementationId,
            Category = dto.Category,
            DurationHours = dto.DurationHours,
            PeriodStart = dto.PeriodStart,
            PeriodEnd = dto.PeriodEnd
        };
    }
}
