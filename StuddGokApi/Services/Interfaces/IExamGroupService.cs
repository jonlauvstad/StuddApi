using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StudentResource.Models.POCO;
using System.Data;

namespace StuddGokApi.Services.Interfaces;

public interface IExamGroupService
{
    Task<IEnumerable<ExamGroupDTO>> GetExamGroupsAsync(int? examId, int? userId, string? name);
    Task<IEnumerable<ExamGroupDTO>?> AddExamGroupAsync(int userId, string role, int examId, IEnumerable<ExamGroupDTO> examGroups);
    Task<int> DeleteExamGroupsByExamIdAndNameAsync(int examId, string name);
    Task<ExamGroupDTO?> AddOneExamGroupAsync(int userId, string role, ExamGroupDTO examGroup);
    Task<ExamGroupDTO?> RemoveExamGroupEntryAsync(int userId, string role, int id);
}
