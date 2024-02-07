using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class ExamImplementationService : IExamImplementationService
{
    private readonly IExamImplementationRepository _examImpRepo;
    private readonly IMapper<ExamImplementation, ExamImplementationDTO> _examImpMapper;

    public ExamImplementationService(IExamImplementationRepository examImpRepo, IMapper<ExamImplementation, ExamImplementationDTO> examImpMapper)
    {
        _examImpRepo = examImpRepo;
        _examImpMapper = examImpMapper;
    }

    public async Task<ExamImplementationDTO?> GetExamImplementationByIdAsync(int id)
    {
        ExamImplementation? exImp = await _examImpRepo.GetExamImplementationById(id);
        if(exImp == null) { return null; }
        return _examImpMapper.MapToDTO(exImp);
    }
}
