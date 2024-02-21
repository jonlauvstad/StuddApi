using StudentResource.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.ApiClients.Interfaces;

public interface ICourseClient
{
    Task<Course> GetCourseByIdAsync(int id);
}
