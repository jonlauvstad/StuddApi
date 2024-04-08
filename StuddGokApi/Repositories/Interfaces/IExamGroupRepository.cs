using Microsoft.AspNetCore.Mvc;
using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories.Interfaces;

public interface IExamGroupRepository
{
    Task<IEnumerable<ExamGroup>> GetExamGroupsAsync(int? examId, int? userId, string? name);
    Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null);
    Task<bool> UniqueNameForExam(int examId, string name);
    Task<bool> NoStudentInOtherGroupForExam(int examId, IEnumerable<ExamGroup> examGroups);
    Task<IEnumerable<ExamGroup>> AddExamGroupsAsync(IEnumerable<ExamGroup> examGroups);
    Task<IEnumerable<ExamGroup>> GetMultipleExamGroupsById(IEnumerable<int> examGroupIds);
    Task<int> DeleteExamGroupsByExamIdAndNameAsync(int examId, string name);
    Task<ExamGroup?> AddOneExamGroupAsync(ExamGroup examGroup);
    Task<ExamGroup?> GetExamGroupAsync(int id);
    Task<int> RemoveExamGroupEntryAsync(int id);
}
