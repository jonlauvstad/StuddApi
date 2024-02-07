using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StuddGokApi.Models;

public class Attendance
{
    [Key] public int Id { get; set; }
    [ForeignKey("LectureId")] public int LectureId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public virtual Lecture? Lecture { get; set; }
    public virtual User? User { get; set; }
}
