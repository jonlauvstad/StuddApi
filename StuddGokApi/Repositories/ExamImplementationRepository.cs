using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Renci.SshNet.Messages.Authentication;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.SSE;
using StudentResource.Models.POCO;
using System;

namespace StuddGokApi.Repositories;

public class ExamImplementationRepository : RepositoryBase, IExamImplementationRepository
{
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
        return await IsOwnerOf(userId, role, examId, GetCourseImpId_FromObjectById, courseImplementationId);
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
}
