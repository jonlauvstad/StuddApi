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
        private const string Backend22_24 = "https://gokstadakademietas.sharepoint.com/:f:/s/1.Backend-utviklingH22-V24/EuJdCT9flYROu9ohS-rEpxYB6pcmwt0H6ZNkWhDgtKucuw?e=at716I";
        private const string Backend22_24_video = "https://gokstadakademietas.sharepoint.com/:f:/s/1.Backend-utviklingH22-V24/EuJdCT9flYROu9ohS-rEpxYB6pcmwt0H6ZNkWhDgtKucuw?e=at716I";

        public IEnumerable<StudentResourceModel> GetResourcesForCourse(int courseId)
        {

            var mockResources = new List<StudentResourceModel>
            {
                new StudentResourceModel
                {
                    Id = 1,
                    Title = "Backend Programmering",
                    Description = "Ressurser for Backend 22-24",
                    URL = Backend22_24,
                    CourseId = courseId // kurset settes i flask
                },
                new StudentResourceModel
                {
                    Id = 2,
                    Title = "Backend Programmering",
                    Description = "Ressurser for Backend 22-24. - Video",
                    URL = Backend22_24_video,
                    CourseId = courseId // samme kurs
                }
            };
            return mockResources;
        }
    }
}
