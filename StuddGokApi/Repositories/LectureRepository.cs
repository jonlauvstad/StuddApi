using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using System.Diagnostics;
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

    
    public async Task<IEnumerable<User>> GetTeachersByCourseImplementationId(int courseImpId)
    {
        IEnumerable<TeacherCourse> tcs =  await _dbContext.TeacherCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        return from tc in tcs select tc.User;
    }
    public async Task<IEnumerable<User>> GetProgramTeachersByCourseImplementationId(int courseImpId)
    {
        IEnumerable<ProgramCourse> pcs = await _dbContext.ProgramCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        IEnumerable<int> progImpIds = from pc in pcs select pc.ProgramImplementationId;
        IEnumerable<TeacherProgram> tps = await _dbContext.TeacherPrograms.Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();
        return from tp in tps select tp.User;
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
        if (o is Lecture) { return (Lecture)o; }
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
        return lec;
    }

    public async Task<Lecture?> DeleteLectureByIdAsync(int id)
    {
        Lecture? lecture = await GetLectureById(id);
        if (lecture == null)
        {
            return null;
        }
        int numDeleted = await _dbContext.Lectures.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (numDeleted == 0)
        {
            return null;
        }

        //if (lecture != null) lecture.LectureVenues = await _dbContext.LectureVenues.Where(x => x.LectureId == id).ToListAsync();
      
        return lecture;
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
        
        return false;
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

    
}
