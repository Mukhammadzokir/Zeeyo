using Zeeyo.Domain.Entities.Branches;

namespace Zeeyo.Service.DTOs.Courses.Courses;

partial class CourseForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<LessonForResultDto> Lessons { get; set; }
    public ICollection<EnrollmentForResultDto> Students { get; set; }
    public ICollection<AttendanceForResultDto> Attendances { get; set; }
    public ICollection<TeacherCourseForResultDto> Teachers { get; set; }
}
