using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using StudentResource.Models.POCO;

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
        if (!await _examRepository.IsOwner(userId, role, examDTO.Id, courseImplementationId: examDTO.CourseImplementationId)) return null;

        Exam? exam = await _examRepository.AddExamAsync(_mapper.MapToModel(examDTO));
        if (exam == null) return null;
        return _mapper.MapToDTO(exam);
    }

    public async Task<ExamDTO?> DeleteExamAsync(int id, int userId, string role)
    {
        if (!await _examRepository.IsOwner(userId, role, id, courseImplementationId: null)) return null;

        Exam? exam = await _examRepository.DeleteExamAsync(id);
        if (exam == null) return null;
        return _mapper.MapToDTO(exam);
    }

    public async Task<IEnumerable<ExamDTO>> GetAllExamsAsync(int? courseImplementationId)
    {
        return (await _examRepository.GetAllExamsAsync(courseImplementationId)).Select(x=> _mapper.MapToDTO(x));
        // from ex await _examRepository.GetAllExamsAsync(courseImplementationId) select _mapper.MapToDTO(ex); 
    }

    public async Task<ExamDTO?> GetExamAsync(int id)
    {
        Exam? exam = await _examRepository.GetExamAsync(id);
        if (exam == null) return null;
        return _mapper.MapToDTO(exam);
    }

    public async Task<ExamDTO?> UpdateExamAsync(int id, ExamDTO examDTO, int userId, string role)
    {
        if (!await _examRepository.IsOwner(userId, role, id, courseImplementationId: null)) return null;
                                                            // kunne hatt courseImplementationId: examDTO.CourseImplementationId
        Exam? exam = await _examRepository.UpdateExamAsync(id, _mapper.MapToModel(examDTO));
        if (exam == null) return null;
        return _mapper.MapToDTO(exam);
    }
}
