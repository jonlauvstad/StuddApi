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

    public async Task<AssignmentDTO?> AddAssignmentAsync(AssignmentDTO assignment, int userId, string role)
    {
        if (assignment == null)
        {
            throw new ArgumentNullException(nameof(assignment), "Assignment cannot be null.");
        }

        Assignment? a = await _assignmentRepository.AddAssignmentAsync(_assignmentMapper.MapToModel(assignment), userId, role);
        if (a == null) { return null; }
        return _assignmentMapper.MapToDTO(a);
    }
}
