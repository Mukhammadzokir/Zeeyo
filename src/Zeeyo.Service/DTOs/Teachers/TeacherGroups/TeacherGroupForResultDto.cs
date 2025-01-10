using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Service.DTOs.Teachers.TeacherGroups;

public class TeacherGroupForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public User Teacher { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime Date { get; set; }
}