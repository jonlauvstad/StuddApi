using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StuddGokDbContext _context;

        public CourseRepository(StuddGokDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        }
    }
}
