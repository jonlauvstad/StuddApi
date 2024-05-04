using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Renci.SshNet.Messages.Authentication;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;
using StudentResource.Models.POCO;
using System;
using System.Collections.Generic;

namespace StuddGokApi.Repositories;

public class ExamImplementationRepository : RepositoryBase, IExamImplementationRepository
{
    // ABOUT LOGGING: Just logging in the functions that may provide info beyond returning null/false (captured in the service layer)
    //  The logging in the service layer has references to the functions here.

    public ExamImplementationRepository(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger, AlertUserList alertUserList) 
        : base(dbContext, logger, alertUserList)
    {
    }

    public async Task<IEnumerable<ExamImplementation>?> 
        AddExamImplementationsAndUserExamImplementationsAsync(List<ExamImplementation> examImps, List<List<int>> listOfPartipLists)
    {
        // ExamImps OG UserExamImps i ÉN TRANSAKSJON !!

        bool success = true;

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Adde ExamImplementations
                    await _dbContext.ExamImplementations.AddRangeAsync(examImps);
                    await _dbContext.SaveChangesAsync();

                    // Adde UserExamImplementations
                    for (int i=0; i< listOfPartipLists.Count; i++)
                    {
                        await _dbContext.UserExamImplementations.AddRangeAsync(
                            listOfPartipLists[i].Select(x => new UserExamImplementation
                            {
                                UserId = x,
                                ExamImplementationId = examImps[i].Id,
                            })
                        );
                        await _dbContext.SaveChangesAsync();
                    }

                    // Adding alerts to db
                    for (int i = 0; i < listOfPartipLists.Count; i++)
                    {
                        await _dbContext.Alerts.AddRangeAsync(listOfPartipLists[i].
                                Select(x => AlertFromExamImplementation(examImps[i], x, EntityAction.added)));
                        await _dbContext.SaveChangesAsync();
                    }
                    
                    

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    success = false; 
                }
            }
        });

        if (!success) return null;

        // FOR SSE
        //_alertUserList.UserIdList.AddRange(userIds);    => from List to ConcurrentBag
        //foreach (int userId in userIds) { _alertUserList.UserIdList.Add(userId); }        // KOMMUT NÅ
        foreach (int userId in listOfPartipLists.SelectMany(x => x)) { _alertUserList.UserIdList.Add(userId); }

        // Returnere ExamImplementations
        // Legge til UserExamImplementation (s - glemte s'en i modellen)
        foreach(var exImp in examImps) 
        {
            exImp.UserExamImplementation = 
                await _dbContext.UserExamImplementations.Where(x => x.ExamImplementationId == exImp.Id).ToListAsync(); 
        }
        IEnumerable<ExamImplementation> returnExImps = await _dbContext.ExamImplementations
                                                    .Include(e => e.Exam)
                                                    .Include(e => e.Venue)
                                                    .Where(x => examImps.Select(y => y.Id).Contains(x.Id))
                                                    .ToListAsync();
        return returnExImps;
        //return examImps;
    }

    public async Task<bool> DeleteByExamIdAsync(int examId)
    {
        // 0) Getting the exam and userExImps
        Exam? exam = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == examId);
        if(exam == null) return false;
        IEnumerable<UserExamImplementation> userExImps =
            await _dbContext.UserExamImplementations.Where(x => x.ExamImplementation!.ExamId == examId).ToListAsync();

        // 1) Making alerts
        List<Alert> alerts = new List<Alert>();
        foreach (UserExamImplementation ux in userExImps)
        {
            alerts.Add(AlertFromExam(exam, ux.ExamImplementationId, ux.UserId, EntityAction.deleted));
        }

        // 2) Delete examImps & insert alerts
        bool success = true;
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Delete ExamImplementations
                    int numDeleted = await _dbContext.ExamImplementations.Where(x => x.ExamId == examId).ExecuteDeleteAsync();
                    await _dbContext.SaveChangesAsync();
                    if (numDeleted == 0) throw new Exception();
                        
                    // Adding alerts to db
                    await _dbContext.Alerts.AddRangeAsync(alerts);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    success = false;
                }
            }
        });
        if (!success) return false;

        // 3) SSE
        foreach (int userId in userExImps.Select(x => x.UserId)) { _alertUserList.UserIdList.Add(userId); }

        return true;
    }

    public async Task<ExamImplementation?> GetExamImplementationById(int id)
    {
        IEnumerable<ExamImplementation> eis = await _dbContext.ExamImplementations.Where(x => x.Id == id).ToListAsync();
        ExamImplementation? exImp = eis.FirstOrDefault();
        if (exImp == null) return null;
        exImp.UserExamImplementation =
                await _dbContext.UserExamImplementations.Where(x => x.ExamImplementationId == exImp.Id).ToListAsync();
        return exImp;
    }

    public async Task<IEnumerable<ExamImplementation>> GetExamImpsByExamIdAsync(int examId)
    {
        IEnumerable<ExamImplementation> examImps = await _dbContext.ExamImplementations.Where(x => x.ExamId == examId).ToListAsync();
        foreach (var exImp in examImps)
        {
            exImp.UserExamImplementation =
                await _dbContext.UserExamImplementations.Where(x => x.ExamImplementationId == exImp.Id).ToListAsync();
        }
        return examImps;   
    }

    public async Task<bool> IsOwner(int userId, string role, int examId, int? courseImplementationId = null)
    {
        bool isOwner = await IsOwnerOf(userId, role, examId, GetCourseImpId_FromObjectById, courseImplementationId);
        if (!isOwner)
        {
            _logger.LogDebug("Class:{class}, Function:{function}, Msg:{msg},\n\t\tTraceId:{traceId}",
            "ExamImplementationRepository", "IsOwner", $"userId:{userId} role:{role} examId:{examId} gives false", System.Diagnostics.Activity.Current?.Id);
        }
        return isOwner;
    }

    private async Task<int?> GetCourseImpId_FromObjectById(int examId)
    {
        Exam? exam = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id == examId);
        if (exam == null) return null;
        return exam.CourseImplementationId;
    }

    private Alert AlertFromExamImplementation(ExamImplementation exImp, int userId, EntityAction action)
    {
        string actionString = MatchAction(action);
        return new Alert
        {
            UserId = userId,
            Time = DateTime.Now,
            Seen = false,
            Message = $"Eksamen i {exImp.Exam!.CourseImplementation!.Name} har blitt {actionString}.",
            Links = $"/ExamImplementation/{exImp.Id}"
        };
    }

    private Alert AlertFromExam(Exam exam, int examImpId,int userId, EntityAction action)
    {
        string actionString = MatchAction(action);
        return new Alert
        {
            UserId = userId,
            Time = DateTime.Now,
            Seen = false,
            Message = $"Eksamen i {exam.CourseImplementation!.Name} har blitt {actionString}.",
            Links = $"/ExamImplementation/{examImpId}"
        };
    }

    private async Task<IEnumerable<ExamImplementation>> GetExamImpsForProgImpFromExamIdAsync(int examId)
    {
        Exam? exam = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id.Equals(examId));
        if ( exam == null) return Enumerable.Empty<ExamImplementation>();

        int progImpId = 
            (await _dbContext.ProgramCourses.FirstOrDefaultAsync(x => x.CourseImplementationId == exam.CourseImplementationId))!
            .ProgramImplementationId;

        IEnumerable<int> cimpIds = await
            _dbContext.ProgramCourses.Where(x => x.ProgramImplementationId == progImpId).Select(e => e.CourseImplementationId)
            .ToListAsync();

        IEnumerable<int> examIds = await 
            _dbContext.Exams.Where(x => cimpIds.Contains(x.CourseImplementationId)).Select(e => e.Id).ToListAsync();

        return await _dbContext.ExamImplementations.Where(x => examIds.Contains(x.ExamId)).ToListAsync();
    }

    // NEW FUNC
    private async Task<IEnumerable<Lecture>> GetLecturesForProgImpFromExamIdAsync(int examId)
    {
        Exam? exam = await _dbContext.Exams.FirstOrDefaultAsync(x => x.Id.Equals(examId));
        if (exam == null) return Enumerable.Empty<Lecture>();
        int progImpId =
            (await _dbContext.ProgramCourses.FirstOrDefaultAsync(x => x.CourseImplementationId == exam.CourseImplementationId))!
            .ProgramImplementationId;
        IEnumerable<int> cimpIds = await
            _dbContext.ProgramCourses.Where(x => x.ProgramImplementationId == progImpId).Select(e => e.CourseImplementationId)
            .ToListAsync();
        IEnumerable<int> lectureIds = await
            _dbContext.Lectures.Where(x => cimpIds.Contains(x.CourseImplementationId)).Select(e => e.Id).ToListAsync();
        return await _dbContext.Lectures.Where(x => lectureIds.Contains(x.Id)).ToListAsync();
    }

    // NEW FUNC
    private bool NotValidTimeLecture(IEnumerable<Lecture> lectures, ExamImplementationDTO exImpDTO) 
    {
        return lectures.Any(x => x.StartTime < exImpDTO.EndTime && x.EndTime > exImpDTO.StartTime);
    }

    private bool NotValidTime(IEnumerable<ExamImplementation> examImps, ExamImplementationDTO exImpDTO)
    {
        return examImps.Any(x => x.StartTime < exImpDTO.EndTime && x.EndTime > exImpDTO.StartTime);
    }

    public async Task<bool> ValidTime_ProgCourses_ExamImpAsync(int examId, IEnumerable<ExamImplementationDTO> exImpDTOs)
    {
        IEnumerable<ExamImplementation> exImps = await GetExamImpsForProgImpFromExamIdAsync(examId);
        foreach (ExamImplementationDTO exImpDTO in exImpDTOs)
        {
            if (exImpDTO.ExamId != examId) continue;
            if (NotValidTime(exImps, exImpDTO)) return false;
        }

        // NEW SECTION:
        IEnumerable<Lecture> lectures = await GetLecturesForProgImpFromExamIdAsync(examId);     // NEW
        foreach (ExamImplementationDTO exImpDTO in exImpDTOs)                                   // NEW
        {                                                                                       // NEW
            if (NotValidTimeLecture(lectures, exImpDTO)) return false;                          // NEW
            _logger.LogDebug($"lectures.Count(): {lectures.Count()}");
        }                                                                                       // NEW

        return true;
    }

    
}
