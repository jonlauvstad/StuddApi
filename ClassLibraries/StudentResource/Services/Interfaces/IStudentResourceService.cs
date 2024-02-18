using StudentResource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Services.Interfaces
{
    public interface IStudentResourceService
    {
        IEnumerable<StudentResourceModel> GetResourcesForCourse(int courseId);
    }
}
