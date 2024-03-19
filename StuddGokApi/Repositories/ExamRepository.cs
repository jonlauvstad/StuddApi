using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace StuddGokApi.Repositories;

public class ExamRepository : RepositoryBase, IExamRepository
{
    //private readonly StuddGokDbContext _dbContext;
    //private readonly ILogger<ExamRepository> _logger;

    public ExamRepository(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger) : base(dbContext, logger)
    {
    }

    public async Task<Exam?> AddExamAsync(Exam exam)
    {
        EntityEntry e = await _dbContext.Exams.AddAsync(exam);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is Exam) { return (Exam)o; }
        return null;
    }

    public async Task<Exam?> DeleteExamAsync(int id)
    {
        Exam? exam = await GetExamAsync(id);
        if (exam == null) return null;

        int numDeleted = await _dbContext.Exams.Where(x => x.Id == id).ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();    // gpt sier nødvendig, men jeg tror ikke egentlig det

        if (numDeleted == 0)
        {
            return null;
        }
        return exam;
    }

    public async Task<IEnumerable<Exam>> GetAllExamsAsync(int? courseImplementationId)
    {
        return await _dbContext.Exams.Where(x => x.CourseImplementationId == courseImplementationId).ToListAsync();
    }

    public async Task<Exam?> GetExamAsync(int id)
    {
        return await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int?> GetCourseImpId_FromObjectById(int examId)
    {
        Exam? exam = await GetExamAsync(examId);
        if (exam == null) return null;
        return exam.Id;
    }
    public async Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null)
    {
        return await IsOwnerOf(userId, role, examId, GetCourseImpId_FromObjectById, courseImplementationId);

        //return await StaticRepoFuncs.IsOwner(userId, role, examId, _dbContext,
        //                                    GetCourseImpId_FromObjectById,
        //                                    courseImplementationId);
    }

    public async Task<Exam?> UpdateExamAsync(int id, Exam exam)
    {
        Exam? ex = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == id);
        if (ex == null) { return null; }

        ex.CourseImplementationId = exam.CourseImplementationId;
        ex.Category = exam.Category;
        ex.DurationHours = exam.DurationHours;
        ex.PeriodEnd = exam.PeriodStart;
        ex.PeriodEnd = exam.PeriodEnd;

        await _dbContext.SaveChangesAsync();
        return exam;
    }
}
