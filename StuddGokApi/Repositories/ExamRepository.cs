﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;
using System.Reflection.Metadata.Ecma335;

namespace StuddGokApi.Repositories;

public class ExamRepository : RepositoryBase, IExamRepository
{
    //private readonly StuddGokDbContext _dbContext;
    //private readonly ILogger<ExamRepository> _logger;

    public ExamRepository(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger, AlertUserList alertUserList)
        : base(dbContext, logger, alertUserList)
    {
    }

    public async Task<Exam?> AddExamAsync(Exam exam)
    {
        EntityEntry e = await _dbContext.Exams.AddAsync(exam);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is Exam) 
        { 
            Exam ex = (Exam)o;
            int id = ex.Id;
            return await _dbContext.Exams
                .Include(x => x.CourseImplementation)
                .Include(x => x.ExamImplementation)
                .FirstOrDefaultAsync(x => x.Id == id);
            
        }
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

    public async Task<IEnumerable<Exam>> GetAllExamsAsync(int? courseImplementationId, int? userId, string? role)
    {
        IEnumerable<Exam> exams;
        if (courseImplementationId != null)
        {
             exams = await _dbContext.Exams.Where(x => x.CourseImplementationId == courseImplementationId).ToListAsync();
        }
        else
        {
            exams = await _dbContext.Exams.ToListAsync();
        }
        if (userId == null)
            return exams;
        List<Exam> exList = new List<Exam>();
        foreach (Exam exam in exams)
        {
            if (await IsOwner(userId.Value, role!, exam.Id, courseImplementationId: null))
            {
                exList.Add(exam);
            }
        }
        return exList;
    }

    public async Task<Exam?> GetExamAsync(int id)
    {
        return await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int?> GetCourseImpId_FromObjectById(int examId)
    {
        Exam? exam = await GetExamAsync(examId);
        if (exam == null) return null;
        //return exam.Id;
        return exam.CourseImplementationId;
    }
    public async Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null)
    {
        return await IsOwnerOf(userId, role, examId, GetCourseImpId_FromObjectById, courseImplementationId);
    }

    public async Task<Exam?> UpdateExamAsync(int id, Exam exam)
    {
        Exam? ex = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == id);
        if (ex == null) { return null; }

        ex.CourseImplementationId = exam.CourseImplementationId;
        ex.Category = exam.Category;
        ex.DurationHours = exam.DurationHours;
        ex.PeriodStart = exam.PeriodStart;
        ex.PeriodEnd = exam.PeriodEnd;

        await _dbContext.SaveChangesAsync();
        return ex; //exam;
    }
}
