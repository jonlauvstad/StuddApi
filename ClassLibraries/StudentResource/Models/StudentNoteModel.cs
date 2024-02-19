using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Models;

public class StudentNoteModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModifiedDate { get; set;}

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [ForeignKey]
    public CourseImplementation CourseImplementationId { get; set; }

    public int CourseImplementationId { get; set; }
    [ForeignKey("CourseImplementationId")]
    public virtual CourseImplementation CourseImplementation { get; set; }

}
