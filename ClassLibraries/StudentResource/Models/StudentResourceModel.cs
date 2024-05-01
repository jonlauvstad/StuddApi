using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Models;

public class StudentResourceModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public int CourseId { get; set; } // associate resource with course
}


public enum ResourceType
{
    Video,
    Article,
    Website,
}

