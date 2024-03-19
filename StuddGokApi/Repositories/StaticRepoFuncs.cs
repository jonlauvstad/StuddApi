using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;

namespace StuddGokApi.Repositories
{
    public class StaticRepoFuncs
    {
        public static async Task<bool> IsOwner(int userId, string role, int objectId, StuddGokDbContext dbContext, 
            Func<int, Task<int?>> getCourseImpId_FromObjectById,
            int? courseImplementationId = null)
        {
            if (role == "admin") return true;

            int ciId;
            if (courseImplementationId != null) { ciId = courseImplementationId.Value; }
            else
            {
                int? ci_id =  await getCourseImpId_FromObjectById(objectId);
                if (ci_id == null) return false;
                ciId = ci_id.Value;

                //Lecture? l = await GetLectureById(lectureId);
                //if (l == null) { return false; }
                //ciId = l.CourseImplementationId;
            }
            if (role == "teacher")
            {
                IEnumerable<int> courseImpIds = await TeacherCourseImps(userId, dbContext);
                if (courseImpIds.Contains(ciId)) return true;
            }
            return false;
        }


        private static async Task<IEnumerable<int>> TeacherCourseImps(int userId, StuddGokDbContext dbContext)
        {
            IEnumerable<TeacherProgram> teachPrgms = await dbContext.TeacherPrograms.Where(x => x.UserId == userId).ToListAsync();
            IEnumerable<int> progImpIds = from item in teachPrgms select item.ProgramImplementationId;
            IEnumerable<ProgramCourse> progCourses = await dbContext.ProgramCourses
                                                        .Where(x => progImpIds.Contains(x.ProgramImplementationId)).ToListAsync();

            IEnumerable<TeacherCourse> teachCourses = await dbContext.TeacherCourses.Where(x => x.UserId == userId).ToListAsync();
            IEnumerable<int> courseImpIdsT = from item in teachCourses select item.CourseImplementationId;

            IEnumerable<int> courseImpIdsP = from item in progCourses select item.CourseImplementationId;
            List<int> cimpsT = courseImpIdsT.ToList();
            cimpsT.AddRange(courseImpIdsP);
            return cimpsT.Distinct();
        }
    }
}
