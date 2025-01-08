using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Students;

namespace Zeeyo.Domain.Entities.Courses;

public class Group : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<User> Students { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
}
