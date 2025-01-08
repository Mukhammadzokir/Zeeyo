using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Domain.Entities.Teachers;

public class TeacherGroup : Auditable
{
    public long TeacherId { get; set; }
    public User Teacher { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime Date { get; set; }
}