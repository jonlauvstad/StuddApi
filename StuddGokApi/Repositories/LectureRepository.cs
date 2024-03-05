using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services;
using StudentResource.Models.POCO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace StuddGokApi.Repositories;

public class LectureRepository : ILectureRepository
{
    private readonly StuddGokDbContext _dbContext;
    private readonly ILogger<LectureRepository> _logger;

    public LectureRepository(StuddGokDbContext dbContext, ILogger<LectureRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<Lecture?> GetLectureById(int id)
    {
        IEnumerable<Lecture> lectures = await _dbContext.Lectures.Where( x => x.Id == id).ToListAsync();

        Lecture? lecture = lectures.FirstOrDefault();
        if (lecture == null ) { return null; }
        lecture.LectureVenues = await _dbContext.LectureVenues.Where(x => x.LectureId == id).ToListAsync();

        return lecture;
    }

    public async Task<IEnumerable<int>> GetUserIdsByCourseImplementationId(int courseImpId)
    {
        List<int> userIds = (await GetStudentIdsByCourseImplementationId(courseImpId)).ToList();
        userIds.AddRange((await GetTeacherIdsByCourseImplementationId(courseImpId)).ToList());
        userIds.AddRange((await GetProgramTeacherIdsByCourseImplementationId(courseImpId)).ToList());
        return userIds;
    }
    public async Task<IEnumerable<Models.User>> GetStudentsByCourseImplementationId(int courseImpId)
    {
        IEnumerable<ProgramCourse> pcs = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> progImpIds = from pc in pcs select pc.ProgramImplementationId;
        IEnumerable<StudentProgram> sps = await _dbContext.StudentPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        return from sp in sps select sp.User;
    }
    public async Task<IEnumerable<int>> GetStudentIdsByCourseImplementationId(int courseImpId)
    {
        IEnumerable<ProgramCourse> pcs = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> progImpIds = from pc in pcs select pc.ProgramImplementationId;
        IEnumerable<StudentProgram> sps = await _dbContext.StudentPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        return from sp in sps select sp.UserId;
    }
    public async Task<IEnumerable<Models.User>> GetTeachersByCourseImplementationId(int courseImpId)
    {
        IEnumerable<TeacherCourse> tcs =  await _dbContext.TeacherCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        return from tc in tcs select tc.User;
    }
    public async Task<IEnumerable<int>> GetTeacherIdsByCourseImplementationId(int courseImpId)
    {
        IEnumerable<TeacherCourse> tcs = await _dbContext.TeacherCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        return from tc in tcs select tc.UserId;
    }
    public async Task<IEnumerable<Models.User>> GetProgramTeachersByCourseImplementationId(int courseImpId)
    {
        IEnumerable<ProgramCourse> pcs = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> progImpIds = from pc in pcs select pc.ProgramImplementationId;
        IEnumerable<TeacherProgram> tps = await _dbContext.TeacherPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        return from tp in tps select tp.User;
    }
    public async Task<IEnumerable<int>> GetProgramTeacherIdsByCourseImplementationId(int courseImpId)
    {
        IEnumerable<ProgramCourse> pcs = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> progImpIds = from pc in pcs select pc.ProgramImplementationId;
        IEnumerable<TeacherProgram> tps = await _dbContext.TeacherPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        return from tp in tps select tp.UserId;
    }
    public async Task<Lecture?> CheckTeacher(int courseImpId, DateTime from, DateTime to)
    {
        // Finding the teacher
        IEnumerable<TeacherCourse> tcs = await _dbContext.TeacherCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> userIds = from tc in tcs select tc.UserId;

        // Finding the teacher's lectures
        IEnumerable < TeacherCourse > teacherCourses = await _dbContext.TeacherCourses.Where(x => userIds.Contains(x.UserId)).ToListAsync();
        IEnumerable<int> courseImpIds = from tc in teacherCourses select tc.CourseImplementationId;
        IEnumerable<Lecture> lectures = await _dbContext.Lectures.Where(x => courseImpIds.Contains(x.CourseImplementationId)).ToListAsync();

        // Returning the lecture that overlaps with from/to
        IEnumerable<Lecture> overlaps = lectures.Where(x => x.StartTime < to && x.EndTime > from);
        if (overlaps.Any()) { return overlaps.FirstOrDefault(); }
        return null;
    }

    public async Task<Lecture?> AddLectureAsync(Lecture lecture)
    {
        
        EntityEntry e = await _dbContext.Lectures.AddAsync(lecture);
        await _dbContext.SaveChangesAsync();
        object o = e.Entity;
        if (o is Lecture) 
        {
            // NEW: Adding alerts to db
            await AddAlertsAsync(
                (from userId in await GetUserIdsByCourseImplementationId(((Lecture)o).CourseImplementationId)
                 select AlertFromLecture((Lecture)o, userId, LectureAction.added)).ToList());

            return (Lecture)o; 
        }
        return null;

    }

    public async Task<(Lecture?, LectureVenue?)> AddLectureAndVenueAsync(Lecture lecture, int venueId)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        Lecture? l = null;
        LectureVenue? lec_ven = null;

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    EntityEntry e = await _dbContext.Lectures.AddAsync(lecture);
                    await _dbContext.SaveChangesAsync();
                    l = lecture;
                    int lectureId = lecture.Id;

                    LectureVenue lv = new LectureVenue
                    {
                        LectureId = lectureId,
                        VenueId = venueId,
                    };
                    EntityEntry ev = await _dbContext.LectureVenues.AddAsync(lv);
                    await _dbContext.SaveChangesAsync();
                    lec_ven = lv;

                    // NEW: Adding alerts to db
                    await AddAlertsAsync(
                        (from userId in await GetUserIdsByCourseImplementationId(l.CourseImplementationId)
                         select AlertFromLecture(l, userId, LectureAction.added)).ToList());

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        });
        
        return (l, lec_ven);        
    }

    public async Task<IEnumerable<Lecture>?> AddMultipleAsync(IEnumerable<Lecture> lectures, IEnumerable<IEnumerable<int>> venueIds)
    {
        // Null values for lectures and corrsponding venueIds to return after ExecutionStrategy (Transaction)
        // - to have variables to return, and to be null if something fails.
        IEnumerable<Lecture>? returnLectures = null;

        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Adding lectureVenues
                    List<Lecture> lec_list = lectures.ToList();
                    List<IEnumerable<int>> venId_list = venueIds.ToList();
                    for (int i = 0; i < lectures.Count(); i++)
                    {
                        await _dbContext.Lectures.AddAsync(lec_list[i]);
                        await _dbContext.SaveChangesAsync();

                        if (venId_list[i].Any())
                        {
                            LectureVenue lecVen = new LectureVenue { LectureId = lec_list[i].Id, VenueId = venId_list[i].FirstOrDefault() };
                            await _dbContext.LectureVenues.AddAsync(lecVen);
                            await _dbContext.SaveChangesAsync();
                            lecVen.Venue = _dbContext.Venues.FirstOrDefault(x => x.Id == lecVen.VenueId);
                            lec_list[i].LectureVenues.Add(lecVen);
                        }
                    }

                    // NEW: Adding alerts
                    List<Alert> alerts = await AlertsFromMultipleLecturesAsync(lec_list, LectureAction.added);
                    await AddAlertsAsync(alerts);

                    transaction.Commit();
                    returnLectures = lec_list;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        });
        return returnLectures;
    }


    public async Task<Lecture?> UpdateLectureAndVenueAsync(Lecture lecture, int venueId)    // venueId is existing lecture's venue (or 0)
    {                                                                                       // it has been extracted from the venueDTO in Service
        Lecture? l = null;                                                                  // necessary to to this because Lecture returned from
        LectureVenue? lecVen = null;                                                        // mapper has only empty hash set of lectureVenues

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //LectureVenue? lecVen =                          // finding existing lecture's venue
                    //await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.VenueId == venueId && x.LectureId == lecture.Id) ?? null;
                    lecVen =                          // finding existing lecture's venue
                    await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.LectureId == lecture.Id);


                    if (lecVen == null)                             // 1 meaning the original had no venue
                    {
                        if (venueId != 0)                           // 1-A but the new one has a venue
                        {
                            lecVen = new LectureVenue { VenueId = venueId, LectureId = lecture.Id };
                            EntityEntry ev = await _dbContext.LectureVenues.AddAsync(lecVen);
                            await _dbContext.SaveChangesAsync();
                        }
                                                                    // 1-B in the case that venueId==null meaning the new lecture also has
                                                                    // no venue,nothing has to be done
                    }
                    else                                            // 2 the old lecture has a venue
                    {
                        if (venueId == 0)                           // 2-A but the new one has no venue
                        {
                            // DELETE lectureVenue
                            int numDeleted = await _dbContext.LectureVenues.Where(x => x.Id == lecVen.Id).ExecuteDeleteAsync();
                            await _dbContext.SaveChangesAsync();     
                            if (numDeleted == 0) { throw new Exception(); }
                        }
                        else                                        // 2-B both old and new have venue
                        {
                            lecVen.VenueId = venueId;
                            lecVen.LectureId = lecture.Id;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    Lecture? lec = await _dbContext.Lectures.FirstOrDefaultAsync(x => x.Id == lecture.Id) ?? null;
                    if (lec == null) { throw new Exception(); }

                    lec.CourseImplementationId = lecture.CourseImplementationId;
                    lec.Theme = lecture.Theme;
                    lec.Description = lecture.Description;
                    lec.StartTime = lecture.StartTime;
                    lec.EndTime = lecture.EndTime;
                    await _dbContext.SaveChangesAsync();
                    l = lec;

                    // NEW: Adding alerts to db
                    await AddAlertsAsync(
                        (from userId in await GetUserIdsByCourseImplementationId(lec.CourseImplementationId)
                         select AlertFromLecture(lec, userId, LectureAction.updated)).ToList());

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        });
        if (l != null && venueId !=0)
        {
            lecVen.Venue = await _dbContext.Venues.FirstOrDefaultAsync(x => x.Id == venueId);
            l.LectureVenues.Add(lecVen);        
        }
            return l;
    }

   
    public async Task<Lecture?> UpdateLectureAsync(Lecture lecture)
    {
        // The null-coalescing operator ?? returns the value of its left-hand operand if it isn't null;
        // otherwise, it evaluates the right-hand operand and returns its result.
        Lecture? lec = await _dbContext.Lectures.FirstOrDefaultAsync(x => x.Id == lecture.Id) ?? null;
        if (lec == null) { return null; }

        lec.CourseImplementationId = lecture.CourseImplementationId;
        lec.Theme = lecture.Theme;
        lec.Description = lecture.Description;
        lec.StartTime = lecture.StartTime;
        lec.EndTime = lecture.EndTime;

        await _dbContext.SaveChangesAsync();

        // NEW: Sending alerts
        await AddAlertsAsync(
            (from userId in await GetUserIdsByCourseImplementationId(lecture.CourseImplementationId)
            select AlertFromLecture(lecture, userId, LectureAction.updated)).ToList());

        return lec;
    }

    public async Task<Lecture?> DeleteLectureByIdAsync(int id)
    {
        Lecture? lecture = await GetLectureById(id);
        if (lecture == null)
        {
            return null;
        }

        // NEW: Find all users and create alerts for them all - wait until successfully deleted lecture before inserting into db
        IEnumerable<int> userIds = await GetUserIdsByCourseImplementationId(lecture.CourseImplementationId);
        List<Alert> alerts = new List<Alert>();
        foreach(int userId in userIds) { alerts.Add(AlertFromLecture(lecture, userId, LectureAction.deleted)); }

        int numDeleted = await _dbContext.Lectures.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (numDeleted == 0)
        {
            return null;
        }

        // NEW: Inserting the alerts into the database
        await AddAlertsAsync(alerts);

        return lecture;
    }

    public async Task<IEnumerable<Lecture>> GetLecturesAsync(DateTime? startAfter, DateTime? endBy, int? courseImpId, int? venueId, int? teacherId)
    {
        if (startAfter == null) startAfter = DateTime.Now;
        if (endBy == null) endBy = DateTime.MaxValue;

        IEnumerable<Lecture> lectures = await _dbContext.Lectures.Where(x => x.StartTime > startAfter && x.EndTime < endBy).ToListAsync();
        if (courseImpId != null)  lectures = lectures.Where(x => x.CourseImplementationId == courseImpId);
        
        if (venueId != null)
        {
            IEnumerable<LectureVenue> lecVens = await _dbContext.LectureVenues.Where(x => x.VenueId == venueId).ToListAsync();
            lectures = lectures.Where(x => (from lv in lecVens select lv.LectureId).Contains(x.Id));
        }

        if (teacherId != null)
        {
            IEnumerable<TeacherCourse> tcs = await _dbContext.TeacherCourses.Where(x => x.UserId == teacherId).ToListAsync();
            lectures = lectures.Where(x => (from tc in tcs select tc.CourseImplementationId).Contains(x.CourseImplementationId));
        }

        return lectures;
    }

    public async Task<bool> IsOwner(int userId, string role, int lectureId, int? courseImplementationId=null)
    {
        if (role == "admin") return true;

        int ciId;
        if (courseImplementationId != null) { ciId  = courseImplementationId.Value; }
        else
        {
            Lecture? l = await GetLectureById(lectureId);
            if (l == null) { return false; }
            ciId = l.CourseImplementationId;
        }
        if (role == "teacher")
        {
            IEnumerable<int> courseImpIds = await TeacherCourseImps(userId);
            if (courseImpIds.Contains(ciId)) return true;
        }
        _logger.LogDebug("IsOwner returning false");
        return false;
    }

    public async Task<IEnumerable<Lecture>?> DeleteMultipleAsync(IEnumerable<int> ids)
    {
        int numLectures = ids.Count();
        IEnumerable<Lecture>? deletedLectures = null;

        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    deletedLectures = await _dbContext.Lectures.Where(x => ids.Contains(x.Id)).ToListAsync();

                    // NEW ALERTS!!
                    List<Alert> alerts = await AlertsFromMultipleLecturesAsync(deletedLectures, LectureAction.deleted);

                    int numDeleted = await _dbContext.Lectures.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync();
                    await _dbContext.SaveChangesAsync();
                    if (numDeleted != numLectures) { throw new Exception(); }

                    // NEW: ALERTS!!
                    await AddAlertsAsync(alerts); 

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        });
        return deletedLectures;
    }

    private async Task<IEnumerable<int>> TeacherCourseImps(int userId)
    {
        IEnumerable<TeacherProgram> teachPrgms = await _dbContext.TeacherPrograms.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> progImpIds = from item in teachPrgms select item.ProgramImplementationId;
        IEnumerable<ProgramCourse> progCourses = await _dbContext.ProgramCourses
                                                    .Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();

        IEnumerable<TeacherCourse> teachCourses = await _dbContext.TeacherCourses.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> courseImpIdsT = from item in teachCourses select item.CourseImplementationId;

        IEnumerable<int> courseImpIdsP = from item in progCourses select item.CourseImplementationId;
        courseImpIdsT.ToList().AddRange(courseImpIdsP);
        return courseImpIdsT;
    }

    private async Task<Alert> AddAlertAsync(Alert alert)
    {
        await _dbContext.Alerts.AddAsync(alert);
        await _dbContext.SaveChangesAsync();
        return alert;
    }
    private async Task AddAlertsAsync(IEnumerable<Alert> alerts)
    {
        await _dbContext.Alerts.AddRangeAsync(alerts);
        await _dbContext.SaveChangesAsync();
    }

    private Alert AlertFromLecture(Lecture lecture, int userId, LectureAction action)
    {
        string actionString = MatchAction(action);
        return new Alert
        {
            UserId = userId,
            Time = DateTime.Now,
            Seen = false,
            Message = $"Forelesning i {lecture.CourseImplementation!.Name} har blitt {actionString}. Link: /Lecture/{lecture.Id}"
        };
    }

    private async Task<List<Alert>> AlertsFromMultipleLecturesAsync(IEnumerable<Lecture> lectures, LectureAction action) 
    {
        // Get users and also make list of lectureAndItsUsers                                                               
        List<int> listOfUserIds = new List<int>();                                                                        // listOfUserIds
        List<(Lecture lec, IEnumerable<int> userIds)> lecs_userIds = new List<(Lecture lec, IEnumerable<int> userIds)>(); // (lecture, userIds)
        foreach (Lecture lecture in lectures)
        {
            IEnumerable<int> userIds = await GetUserIdsByCourseImplementationId(lecture.CourseImplementationId);
            lecs_userIds.Add((lecture, userIds));
            listOfUserIds.AddRange(userIds.Where(x => !listOfUserIds.Contains(x)));
        };

        // Make individual alert for each user
        List<Alert> alerts = new List<Alert>();
        foreach(int uId in listOfUserIds) alerts.Add(OneAlertFromMultipleLectures(lecs_userIds, uId, action));
        return alerts;
        
    }
    private Alert OneAlertFromMultipleLectures(List<(Lecture lec, IEnumerable<int> userIds)> lecs_userIds, int userId, LectureAction action)
    {
        List<Lecture> lecs = (from luid in lecs_userIds where luid.userIds.Contains(userId) select luid.lec).ToList();
        List<string> courseImpNames = (from lec in lecs select lec.CourseImplementation!.Name).Distinct().ToList();

        StringBuilder sb = new StringBuilder();
        int l = courseImpNames.Count();
        for (int i = 0; i < l; i++)
        {
            sb.Append(courseImpNames[i]);
            if (i < l - 2) sb.Append(", ");
            else if (i == l - 2) sb.Append(" og ");
        }
        string courseImpString = sb.ToString();

        string actionString = MatchAction(action);

        return new Alert
        {
            UserId = userId,
            Time = DateTime.Now,
            Seen = false,
            Message = $"Forelesninger i {courseImpString} har blitt {actionString}. Antall påvirkede forelesninger: {lecs.Count}. " +
                $"Sjekk kalenderen!"
        };

    }

    private string MatchAction(LectureAction action)
    {
        string actionString = "lagt til";
        switch (action)
        {
            case LectureAction.updated:
                actionString = "endret";
                break;
            case LectureAction.deleted:
                actionString = "slettet";
                break;
        }
        return actionString;
    }

    
}

enum LectureAction { added, updated, deleted }