using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Teachers;

namespace Zeeyo.Service.DTOs.Teachers.TeacherCourses;

public class TeacherCourseForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}
