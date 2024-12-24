using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Service.DTOs.Teachers.TeacherCourses;

public class TeacherCourseForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public User Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}
