using StudentResource.Models;
using StudentResource.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Services
{
    public class StudentResourceService : IStudentResourceService
    {
        public IEnumerable<StudentResourceModel> GetResourcesForCourse(int courseId)
        {
            // Mock data for demonstration purposes
            var mockResources = new List<StudentResourceModel>
            {
                new StudentResourceModel
                {
                    Id = 1,
                    Title = "Introduction to Course",
                    Description = "A brief introduction to the course.",
                    URL = "http://example.com/intro",
                    CourseId = courseId // Assuming this is the course you're interested in
                },
                new StudentResourceModel
                {
                    Id = 2,
                    Title = "Advanced Topics",
                    Description = "Deep dive into advanced topics.",
                    URL = "http://example.com/advanced",
                    CourseId = courseId // Assuming this is the same course
                }
            };
            return mockResources;
        }
    }
}
