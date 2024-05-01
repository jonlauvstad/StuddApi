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
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        Assignment? assignment =  await _assignmentRepository.GetAssignmentByIdAsync(id);
        if (assignment == null) 
        { 
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AssignmentService", "GetAssignmentByIdAsync", "_assignmentRepository.GetAssignmentByIdAsync returns null", traceId);

            return null; 
        }
        return _assignmentMapper.MapToDTO(assignment);
    }

    public async Task<AssignmentDTO?> AddAssignmentAsync(AssignmentDTO assignment, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        if (assignment == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AssignmentService", "AddAssignmentAsync", "assignment is null", traceId);

            throw new ArgumentNullException(nameof(assignment), "Assignment cannot be null.");
        }

        Assignment? a = await _assignmentRepository.AddAssignmentAsync(_assignmentMapper.MapToModel(assignment), userId, role);
        if (a == null) 
        { 
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AssignmentService", "AddAssignmentAsync", "_assignmentRepository.AddAssignmentAsync returns null", traceId);

            return null; 
        }
        return _assignmentMapper.MapToDTO(a);
    }

    public async Task<(bool success, string message, AssignmentDTO? assignmentDTO)> UpdateAssignmentAsync(int id, AssignmentDTO assignmentDTO, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;
        _logger.LogDebug("Starting update for assignment with ID: {id}, TraceId: {traceId}", id, traceId);

        // Call to the repository method that now returns a tuple
        var (success, message, assignment) = await _assignmentRepository.UpdateAssignmentAsync(id, _assignmentMapper.MapToModel(assignmentDTO), userId, role);

        if (!success)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg}, TraceId:{traceId}",
                             "AssignmentService", "UpdateAssignmentAsync", message, traceId);
            return (false, message, null);
        }

        _logger.LogInformation("Successfully updated assignment with ID: {id}, TraceId: {traceId}", id, traceId);
        return (true, "Assignment updated successfully", _assignmentMapper.MapToDTO(assignment));
    }


    //public async Task<AssignmentDTO?> UpdateAssignmentAsync(int id, AssignmentDTO assignmentDTO, int userId, string role)
    //{
    //    string? traceId = System.Diagnostics.Activity.Current?.Id;

    //    Assignment? a = await _assignmentRepository.UpdateAssignmentAsync(id, _assignmentMapper.MapToModel(assignmentDTO), userId, role);
    //    if (a == null)
    //    {
    //        _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
    //            "AssignmentService", "UpdateAssignmentAsync", "_assignmentRepository.UpdateAssignmentAsync returns null", traceId);

    //        return null;
    //    }
    //    return _assignmentMapper.MapToDTO(a);
    //}

    public async Task<AssignmentDTO?> DeleteAssignmentAsync(int id, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        Assignment? a = await _assignmentRepository.DeleteAssignmentAsync(id, userId, role);

        if (a == null  ) 
        { 
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "AssignmentService", "DeleteAssignmentAsync", "_assignmentRepository.DeleteAssignmentAsync returns null", traceId);

            return null; 
        }
        return _assignmentMapper.MapToDTO(a);
    }
}
