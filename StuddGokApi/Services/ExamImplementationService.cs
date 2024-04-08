using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using System.Linq;

namespace StuddGokApi.Services;

public class ExamImplementationService : IExamImplementationService
{
    private readonly IExamImplementationRepository _examImpRepo;
    private readonly IMapper<ExamImplementation, ExamImplementationDTO> _examImpMapper;
    private readonly IVenueRepository _venueRepository;
    private readonly ILogger<ExamImplementationService> _logger;

    public ExamImplementationService(IExamImplementationRepository examImpRepo, IVenueRepository venueRepository,
        IMapper<ExamImplementation, ExamImplementationDTO> examImpMapper, ILogger<ExamImplementationService> logger)
    {
        _examImpRepo = examImpRepo;
        _examImpMapper = examImpMapper;
        _venueRepository = venueRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<ExamImplementationDTO>> GetExamImpsByExamIdAsync(int examId)
    {
        return (await _examImpRepo.GetExamImpsByExamIdAsync(examId)).Select(x => _examImpMapper.MapToDTO(x));
        //return from exImp in await _examImpRepo.GetExamImpsByExamIdAsync(examId) select _examImpMapper.MapToDTO(exImp);
    }

    public async Task<ExamImplementationDTO?> GetExamImplementationByIdAsync(int id)
    {
        ExamImplementation? exImp = await _examImpRepo.GetExamImplementationById(id);
        if(exImp == null) { return null; }
        return _examImpMapper.MapToDTO(exImp);
    }

    public async Task<IEnumerable<ExamImplementationDTO>?> 
        AddExamImplementationsAndUserExamImplementationsAsync(int userId, string role, IEnumerable<ExamImplementationDTO> examImpDTOs)
    {
        // IsOwner
        foreach (ExamImplementationDTO exImpDTO in examImpDTOs)
            {
                bool isOwner = await _examImpRepo.IsOwner(userId, role, exImpDTO.ExamId);
                if (!isOwner) return null;
            }


        // Not Exam for 'ProgramCourses' simultaniously


        // CheckVenue 
        foreach (ExamImplementationDTO exImpDTO in examImpDTOs) 
        {
            var resCheck = await _venueRepository.CheckVenueAsync(exImpDTO.VenueId, exImpDTO.StartTime, exImpDTO.EndTime);
            if (resCheck != null) return null;
        }


        List<ExamImplementation> exImps = examImpDTOs.Select(x => _examImpMapper.MapToModel(x)).ToList();
        List<List<int>> listOfPartipLists = examImpDTOs.Select(x => x.ParticipantIds).ToList();
        IEnumerable<ExamImplementation>? examImps = await _examImpRepo.AddExamImplementationsAndUserExamImplementationsAsync(exImps, listOfPartipLists);
        if (examImps == null) return null;
        return examImps.Select(x => _examImpMapper.MapToDTO(x));
    }
}
