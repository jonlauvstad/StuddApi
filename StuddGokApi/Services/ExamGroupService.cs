using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;
using StudentResource.Models.POCO;
using System.Data;

namespace StuddGokApi.Services;

public class ExamGroupService : IExamGroupService
{
    private readonly IExamGroupRepository _exGroupRepository;
    private readonly IMapper<ExamGroup, ExamGroupDTO> _examGroupMapper;
    private readonly ILogger<ExamGroupService> _logger;

    public ExamGroupService(IExamGroupRepository exGroupRepository, IMapper<ExamGroup, ExamGroupDTO> examGroupMapper, ILogger<ExamGroupService> logger)
    {
        _exGroupRepository = exGroupRepository;
        _examGroupMapper = examGroupMapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ExamGroupDTO>?> AddExamGroupAsync(int userId, string role,
                                                                    int examId, IEnumerable<ExamGroupDTO> examGroups)
    {
        if (!await _exGroupRepository.IsOwner(userId, role, examId)) return null;
        if (!await _exGroupRepository.NoStudentInOtherGroupForExam(examId, examGroups.Select(x => _examGroupMapper.MapToModel(x)))) 
            {  return null; }
        string? name = AllNamesEqual(examGroups);
        if (name == null) return null;
        if (!await _exGroupRepository.UniqueNameForExam(examId, name)) return null;

        return (await _exGroupRepository.AddExamGroupsAsync(examGroups.Select(x => _examGroupMapper.MapToModel(x))))
                .Select(x => _examGroupMapper.MapToDTO(x));
    }

    public async Task<ExamGroupDTO?> AddOneExamGroupAsync(int userId, string role, ExamGroupDTO examGroup)
    {
        if (!await _exGroupRepository.IsOwner(userId, role, examGroup.ExamId)) return null;
        if (!(await _exGroupRepository.GetExamGroupsAsync(examId: examGroup.ExamId, userId: null, name: examGroup.Name)).Any())
            return null;
        if ((await _exGroupRepository.GetExamGroupsAsync(examId: null, userId: examGroup.UserId, name: null)).Any())
            return null;

        ExamGroup? exGr = await _exGroupRepository.AddOneExamGroupAsync(_examGroupMapper.MapToModel(examGroup));
        if (exGr == null) return null;
        return _examGroupMapper.MapToDTO(exGr);
    }

    public Task<int> DeleteExamGroupsByExamIdAndNameAsync(int examId, string name)
    {
        return _exGroupRepository.DeleteExamGroupsByExamIdAndNameAsync(examId, name);
    }

    public async Task<IEnumerable<ExamGroupDTO>> GetExamGroupsAsync(int? examId, int? userId, string? name)
    {
        return (await _exGroupRepository.GetExamGroupsAsync(examId, userId, name)).Select(x => _examGroupMapper.MapToDTO(x));
    }

    public async Task<ExamGroupDTO?> RemoveExamGroupEntryAsync(int userId, string role, int id)
    {
        ExamGroup? examGroup = await _exGroupRepository.GetExamGroupAsync(id);
        if (examGroup == null) return null;

        if (! await _exGroupRepository.IsOwner(userId, role, examGroup.ExamId)) return null;

        if (await _exGroupRepository.RemoveExamGroupEntryAsync(id) == 1) return _examGroupMapper.MapToDTO(examGroup);
        return null;
    }

    private string? AllNamesEqual(IEnumerable<ExamGroupDTO> examGroups)
    {
        IEnumerable<string> names = examGroups.Select(x => x.Name);
        string? name = names.FirstOrDefault(); 
        if (name == null) return null;
        if (names.Any(x => x != name)) return null;
        return name;
    }
}
