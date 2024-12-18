using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Domain.Entities.Students;

public class Attendance : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; }
}
