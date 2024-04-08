using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.SSE;

namespace StuddGokApi.Repositories;

public class RepositoryBase 
{
    protected readonly StuddGokDbContext _dbContext;
    protected readonly ILogger<RepositoryBase> _logger;
    public AlertUserList _alertUserList;

    public RepositoryBase(StuddGokDbContext dbContext, ILogger<RepositoryBase> logger, AlertUserList alertUserList)
    {
        _dbContext = dbContext;
        _logger = logger;
        _alertUserList = alertUserList;
    }

    public async Task<bool> IsOwnerOf(int userId, string role, int objectId, //StuddGokDbContext dbContext,
        Func<int, Task<int?>> getCourseImpId_FromObjectById,
        int? courseImplementationId = null)
    {
        if (role == "admin") return true;

        int ciId;
        if (courseImplementationId != null) { ciId = courseImplementationId.Value; }
        else
        {
            int? ci_id = await getCourseImpId_FromObjectById(objectId);
            if (ci_id == null) return false;
            ciId = ci_id.Value;

            //Lecture? l = await GetLectureById(lectureId);
            //if (l == null) { return false; }
            //ciId = l.CourseImplementationId;
        }
        if (role == "teacher")
        {
            IEnumerable<int> courseImpIds = await TeacherCourseImps(userId);     // ,dbContext
            if (courseImpIds.Contains(ciId)) return true;
        }
        return false;
    }


    private async Task<IEnumerable<int>> TeacherCourseImps(int userId)          // , StuddGokDbContext dbContext
    {
        IEnumerable<TeacherProgram> teachPrgms = await _dbContext.TeacherPrograms.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> progImpIds = from item in teachPrgms select item.ProgramImplementationId;
        IEnumerable<ProgramCourse> progCourses = await _dbContext.ProgramCourses
                                                    .Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();

        IEnumerable<TeacherCourse> teachCourses = await _dbContext.TeacherCourses.Where(x => x.UserId == userId).ToListAsync();
        IEnumerable<int> courseImpIdsT = from item in teachCourses select item.CourseImplementationId;

        IEnumerable<int> courseImpIdsP = from item in progCourses select item.CourseImplementationId;
        List<int> cimpsT = courseImpIdsT.ToList();
        cimpsT.AddRange(courseImpIdsP);
        return cimpsT.Distinct();
    }

    protected string MatchAction(EntityAction action)
    {
        string actionString = "lagt til";
        switch (action)
        {
            case EntityAction.updated:
                actionString = "endret";
                break;
            case EntityAction.deleted:
                actionString = "slettet";
                break;
        }
        return actionString;
    }
}

public enum EntityAction { added, updated, deleted }