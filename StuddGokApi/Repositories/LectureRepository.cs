﻿using Castle.Core.Logging;
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

    public LectureRepository(StuddGokDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Lecture?> GetLectureById(int id)
    {
        IEnumerable<Lecture> lectures = await _dbContext.Lectures.Where( x => x.Id == id).ToListAsync();

        Lecture? lecture = lectures.FirstOrDefault();
        if (lecture == null ) { return null; }
        lecture.LectureVenues = await _dbContext.LectureVenues.ToListAsync();

        return lecture;
    }

    
    public async Task<IEnumerable<User>> GetTeachersByCourseImplementationId(int courseImpId)
    {
        IEnumerable<TeacherCourse> tcs =  await _dbContext.TeacherCourses.Where(x => x.CourseImplementationId == courseImpId).ToListAsync();
        return from tc in tcs select tc.User;
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
                    //throw;
                }
            }
        });
        return (l, lec_ven);        
    }

    public async Task<Lecture?> UpdateLectureAndVenueAsync(Lecture lecture, int venueId)
    {
        Lecture? l = null;

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    LectureVenue? lecVen =
                    await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.VenueId == venueId && x.LectureId == lecture.Id) ?? null;
                    if (lecVen == null) 
                    {
                        
                        if (venueId == 0)
                        {
                            // SLETTE lectureVenue
                            LectureVenue? lv = await _dbContext.LectureVenues.FirstOrDefaultAsync(x => x.Id == lecture.Id) ?? null;
                            if (lv != null)
                            {
                                int numDeleted = await _dbContext.Lectures.Where(x => x.Id == lv.Id).ExecuteDeleteAsync();
                                if (numDeleted == 0) { throw new Exception(); }
                            }
                        }

                        lecVen = new LectureVenue { Id = venueId, LectureId=lecture.Id };
                        EntityEntry ev = await _dbContext.LectureVenues.AddAsync(lecVen);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        lecVen.VenueId = venueId;
                        lecVen.LectureId = lecture.Id;
                        await _dbContext.SaveChangesAsync();
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
        return lecture;
    }
}
