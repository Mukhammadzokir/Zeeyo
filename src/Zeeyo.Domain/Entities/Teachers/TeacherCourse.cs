using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Domain.Entities.Teachers;

public class TeacherCourse : Auditable
{
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}