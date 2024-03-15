using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers;

public class AssignmentMapper : IMapper<Assignment, AssignmentDTO>
{
    public AssignmentDTO MapToDTO(Assignment model)
    {
        AssignmentDTO aDTO = new()
        //return new AssignmentDTO
        {
            Id = model.Id,
            CourseImplementationId = model.CourseImplementationId,
            Name = model.Name,
            Description = model.Description,
            Deadline = model.Deadline,
            Mandatory = model.Mandatory,

            AssignmentResults = model.AssignmentResults,
        };
        if (model.CourseImplementation != null)
        {
            aDTO.CourseImplementationName = model.CourseImplementation!.Name;
            aDTO.CourseImplementationCode = model.CourseImplementation.Code;
        }
        return aDTO;
    }

    public Assignment MapToModel(AssignmentDTO dto)
    {
        return new Assignment
        {
            Id = dto.Id,
            CourseImplementationId = dto.CourseImplementationId,
            Name = dto.Name,
            Description = dto.Description,
            Deadline = dto.Deadline,
            Mandatory = dto.Mandatory,
        };
    }
}
