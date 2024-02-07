using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IMapper<Assignment, AssignmentDTO> _assignmentMapper;

    public AssignmentService(IAssignmentRepository assignmentRepository, IMapper<Assignment, AssignmentDTO> assignmentMapper)
    {
        _assignmentRepository = assignmentRepository;
        _assignmentMapper = assignmentMapper;
    }
    public async Task<AssignmentDTO?> GetAssignmentByIdAsync(int id)
    {
        Assignment? assignment =  await _assignmentRepository.GetAssignmentById(id);
        if (assignment == null) { return null; }
        return _assignmentMapper.MapToDTO(assignment);
    }
}
