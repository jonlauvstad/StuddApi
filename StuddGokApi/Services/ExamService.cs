using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using StudentResource.Models.POCO;
using System.Diagnostics;

namespace StuddGokApi.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IMapper<Exam, ExamDTO> _mapper;
    private readonly ILogger<ExamService> _logger;  

    public ExamService(IExamRepository examRepository, IMapper<Exam, ExamDTO> mapper, ILogger<ExamService> logger)
    {
        _examRepository = examRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ExamDTO?> AddExamAsync(ExamDTO examDTO, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;



        if (!await _examRepository.IsOwner(userId, role, examDTO.Id, courseImplementationId: examDTO.CourseImplementationId))
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "AddExamAsync", "_examRepository.IsOwner returns false", traceId);
            
            return null;
        }

        Exam? exam = await _examRepository.AddExamAsync(_mapper.MapToModel(examDTO));

        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "AddExamAsync", "_examRepository.AddExamAsync returns null", traceId);

            return null;
        }
        return _mapper.MapToDTO(exam);
    }

    public async Task<ExamDTO?> DeleteExamAsync(int id, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;

        if (!await _examRepository.IsOwner(userId, role, id, courseImplementationId: null))
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "DeleteExamAsync", "_examRepository.IsOwner returns false", traceId);

            return null;
        }

        Exam? exam = await _examRepository.DeleteExamAsync(id);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "DeleteExamAsync", "_examRepository.DeleteExamAsync returns null", traceId); 

            return null;
        }
        return _mapper.MapToDTO(exam);
    }

    public async Task<IEnumerable<ExamDTO>> GetAllExamsAsync(int? courseImplementationId, int? userId, string? role)
    {
        return (await _examRepository.GetAllExamsAsync(courseImplementationId, userId, role)).Select(x=> _mapper.MapToDTO(x));
        // from ex await _examRepository.GetAllExamsAsync(courseImplementationId) select _mapper.MapToDTO(ex); 
    }

    public async Task<ExamDTO?> GetExamAsync(int id)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;


        Exam? exam = await _examRepository.GetExamAsync(id);
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "GetExamAsync", "_examRepository.GetExamAsync returns null", traceId);

            return null;
        }
        return _mapper.MapToDTO(exam);
    }

    public async Task<ExamDTO?> UpdateExamAsync(int id, ExamDTO examDTO, int userId, string role)
    {
        string? traceId = System.Diagnostics.Activity.Current?.Id;


        if (!await _examRepository.IsOwner(userId, role, id, courseImplementationId: null))
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "UpdateExamAsync", "_examRepository.IsOwner returns false", traceId);

            return null;
        }
                                                            // kunne hatt courseImplementationId: examDTO.CourseImplementationId
        Exam? exam = await _examRepository.UpdateExamAsync(id, _mapper.MapToModel(examDTO));
        if (exam == null)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
                "ExamService", "UpdateExamAsync", "_examRepository.UpdateExamAsync returns null", traceId);

            return null;
        }
        return _mapper.MapToDTO(exam);
    }
}
