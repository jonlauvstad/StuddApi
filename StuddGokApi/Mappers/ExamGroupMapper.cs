using Microsoft.EntityFrameworkCore;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StudentResource.Models.POCO;

namespace StuddGokApi.Mappers;

public class ExamGroupMapper : IMapper<ExamGroup, ExamGroupDTO>
{
    private readonly ILogger<ExamGroupMapper> _logger;
    public ExamGroupMapper(ILogger<ExamGroupMapper> logger)
    {
        _logger = logger;
    }

    public ExamGroupDTO MapToDTO(ExamGroup model)
    {
        ExamGroupDTO dto = new ExamGroupDTO()
        {
            Id = model.Id,
            ExamId = model.ExamId,
            UserId = model.UserId,
            Name = model.Name,

            //CourseImplementationId = model.Exam!.CourseImplementationId,
            //CourseImplementationLink = $"/CourseImplementation/{model.Exam.CourseImplementationId}",
            //CourseImplementationCode = model.Exam.CourseImplementation!.Code,
            //CourseImplementationName = model.Exam.CourseImplementation.Name,
            //FirstName = model.User!.FirstName,
            //LastName = model.User.LastName,
            //GokstadEmail = model.User.GokstadEmail
        };
        if (model.Exam != null)
        {
            dto.CourseImplementationId = model.Exam.CourseImplementationId;
            dto.CourseImplementationLink = $"/CourseImplementation/{model.Exam.CourseImplementationId}";
            if (model.Exam.CourseImplementation != null)
            {
                dto.CourseImplementationCode = model.Exam.CourseImplementation.Code;
                dto.CourseImplementationName = model.Exam.CourseImplementation.Name;
            }
        }
        else { _logger.LogDebug("Exam is null!"); }
        if (model.User != null)
        {
            dto.FirstName = model.User.FirstName;
            dto.LastName = model.User.LastName;
            dto.GokstadEmail = model.User.GokstadEmail;
        }
        else { _logger.LogDebug("User is null!"); }
        return dto;
    }

    public ExamGroup MapToModel(ExamGroupDTO dto)
    {
        return new ExamGroup
        {
            Id = dto.Id,
            ExamId = dto.ExamId,
            UserId = dto.UserId,
            Name = dto.Name,
        };
    }
}
