using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Service.DTOs.Courses.Lessons;
using Zeeyo.Service.DTOs.Students.Attendances;
using Zeeyo.Service.DTOs.Students.Enrollments;
using Zeeyo.Service.DTOs.Teachers.TeacherGroups;

namespace Zeeyo.Service.DTOs.Courses.Groups;

public class GroupForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<LessonForResultDto> Lessons { get; set; }
    public ICollection<EnrollmentForResultDto> Students { get; set; }
    public ICollection<AttendanceForResultDto> Attendances { get; set; }
    public ICollection<TeacherGroupForResultDto> Teachers { get; set; }
}
