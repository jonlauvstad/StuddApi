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
    private readonly ILogger<AssignmentService> _logger;

    public AssignmentService(IAssignmentRepository assignmentRepository, 
                                IMapper<Assignment, AssignmentDTO> assignmentMapper, 
                                ILogger<AssignmentService> logger)
    {
        _assignmentRepository = assignmentRepository;
        _assignmentMapper = assignmentMapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AssignmentDTO?>> GetAllAssignmentsAsync(int? courseImpId, int? userId, string role)
    {
        return (await _assignmentRepository.GetAllAssignmentsAsync(courseImpId, userId, role )).Select(x => _assignmentMapper.MapToDTO(x)).ToList();
    }

    public async Task<AssignmentDTO?> GetAssignmentByIdAsync(int id)
    {
        Assignment? assignment =  await _assignmentRepository.GetAssignmentByIdAsync(id);
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

    public async Task<AssignmentDTO?> UpdateAssignmentAsync(int id, AssignmentDTO assignmentDTO, int userId, string role)
    {

        Assignment? a = await _assignmentRepository.UpdateAssignmentAsync(id, _assignmentMapper.MapToModel(assignmentDTO), userId, role);
        if (a == null) { return null; }
        return _assignmentMapper.MapToDTO(a);
    }

    public async Task<AssignmentDTO?> DeleteAssignmentAsync(int id, int userId, string role)
    {
        Assignment? a = await _assignmentRepository.DeleteAssignmentAsync(id, userId, role);

        if (a == null  ) { return null; }
        return _assignmentMapper.MapToDTO(a);
    }
}
