using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Models.POCO;

public class CourseImplementation
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

