using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Org.BouncyCastle.Bcpg.Sig;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;
using System.Diagnostics;

namespace StuddGokApi.Repositories;

public class ExamGroupRepository : RepositoryBase, IExamGroupRepository
{
    // ABOUT LOGGING: No logging in this class, since it would not add to the information captured in the service layer
    // - funcs here returning null or false. The logging in the service layer has references to the functions here.

    public ExamGroupRepository(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger, AlertUserList alertUserList)
        : base(dbContext, logger, alertUserList)
    {
    }

    public async Task<IEnumerable<ExamGroup>> AddExamGroupsAsync(IEnumerable<ExamGroup> examGroups)
    {
        await _dbContext.ExamGroups.AddRangeAsync(examGroups);
        await _dbContext.SaveChangesAsync();
        string name = examGroups.FirstOrDefault()!.Name;
        return await _dbContext.ExamGroups
            .Include(x => x.User)
            .Include(x => x.Exam)
            .Where(x => x.Name == name).ToListAsync();
    }

    public async Task<IEnumerable<ExamGroup>> GetExamGroupsAsync(int? examId, int? userId, string? name)
    {
        IEnumerable<ExamGroup> examGroups = await _dbContext.ExamGroups.ToListAsync();
        if (examId != null) examGroups = examGroups.Where(x => x.ExamId == examId);
        if (userId != null) examGroups = examGroups.Where(x => x.UserId == userId);
        if (name != null) examGroups = examGroups.Where(x => x.Name == name);
        return examGroups;
    }

    public async Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null)
    {
        return await IsOwnerOf(userId, role, examId, GetCourseImpId_FromObjectById, courseImplementationId);
    }

    public async Task<bool> NoStudentInOtherGroupForExam(int examId, IEnumerable<ExamGroup> examGroups)
    {
        return ! await _dbContext.ExamGroups.AnyAsync(x => x.ExamId == examId && examGroups.Select(x => x.UserId).Contains(x.UserId));
    }

    public async Task<bool> UniqueNameForExam(int examId, string name)
    {
        return (await _dbContext.ExamGroups.FirstOrDefaultAsync(x => x.ExamId == examId && x.Name == name)) == null;
    }

    public async Task<IEnumerable<ExamGroup>> GetMultipleExamGroupsById(IEnumerable<int> examGroupIds)
    {
        return await _dbContext.ExamGroups.Where(x => examGroupIds.Contains(x.Id)).ToListAsync();
    }

    private async Task<int?> GetCourseImpId_FromObjectById(int examId)
    {
        Exam? exam = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == examId);
        if (exam == null) return null;
        return exam.CourseImplementationId;
    }

    public async Task<int> DeleteExamGroupsByExamIdAndNameAsync(int examId, string name)
    {
        return await _dbContext.ExamGroups.Where(x => x.ExamId == examId && x.Name == name).ExecuteDeleteAsync();
    }

    public async Task<ExamGroup?> AddOneExamGroupAsync(ExamGroup examGroup)
    {
        EntityEntry e = await _dbContext.ExamGroups.AddAsync(examGroup);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is ExamGroup) 
        {
            //return (ExamGroup)o;
            ExamGroup eg = (ExamGroup)o;
            return await _dbContext.ExamGroups
                .Include(x => x.Exam)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id ==  eg.Id);
        }
        return null;
    }

    public async Task<ExamGroup?> GetExamGroupAsync(int id)
    {
        return await _dbContext.ExamGroups
            .Include(x => x.Exam)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> RemoveExamGroupEntryAsync(int id)
    {
        return await _dbContext.ExamGroups.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
